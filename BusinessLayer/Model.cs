using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

// local
using DBid = System.Int32;
using DBpair= System.Collections.Generic.KeyValuePair<int, string>;
using DBpairs = System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, string>>;


namespace BusinessLayer
{
    public class TodayActivity
    {
        public ProfileActivity ProfileActivity;
        public ProfileDetail ProfileDetail;
        public Activity Activity;
        public Subscription Subscription;
        public Program Program;
    }

    public class ClientActivityLog
    {
        public ActivityLog ActivityLog;
        public string ActivityDescription;
        public DateTime SubscriptionStartDate;
        public string ProgramName;
        public string UserName;
    }

    public class ProgramUserAttribute
    {
        public Program Program;
        public bool isOwned;
        public bool isSubscribed;
    }

    public class UserSubscription
    {
        public Subscription subscription;
        public Program program;
    }

    public class Model
    {
        public static bool addMember(Guid uid, string handle, int userType)
        {
            using (var db = new iWorkoutEDMContainer())
            {
                User newUser = new User()
                {
                    Id = uid,
                    Handle = handle,
                };

                UserType userTypeForeign = db.UserTypes.Find(userType);
                userTypeForeign.Users.Add(newUser);
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }          
        }

        public static string getUserType(Guid uid)
        {
            User user;
            using (var db = new iWorkoutEDMContainer())
            {
                user = db.Users.Find(uid);
                try
                {
                    return user.UserTypeKey.Name;
                }
                catch (Exception)
                {
                    return "Unknown Type";
                }
            }
        }

        public static IEnumerable<TodayActivity> loadClientTodayData(Guid uid)
        {
            try
            {
                using (var db = new iWorkoutEDMContainer())
                {
                User user = db.Users.Find(uid);
                /* 
                   //This is Entity Framework version of Join
                 * user.Subscriptions.Where(s => s.IsActive == true)
                    .Join(db.Programs, 
                      subs => subs.ProgramKey.Id,
                      prog => prog.Id,
                      (subs, prog) => new { Subscription = subs, Program = prog})
                    .Select(innerSub => innerSub.Program); 
                 */

               var queryResults = from s in user.Subscriptions.Where(s => s.IsActive == true)
                                  join p in db.Programs on s.ProgramKey.Id equals p.Id
                                   join pp in db.ProgramProfiles on s.ProgramKey.Id equals pp.ProgramKey.Id
                                   join pd in db.ProfileDetails on pp.ProfileDetailKey.Id equals pd.Id
                                   join pa in db.ProfileActivities.Where(pa => pa.IsActive == true) on pd.Id equals pa.ProfileDetailKey.Id
                                   join act in db.Activities on pa.ActivityKey.Id equals act.Id
                                  where (pd.CycleRepeatCount == -1 ?
                                          ((DateTime.Now.Subtract(s.StartDate).Days % pd.CycleLength) == pa.Priority)
                                          :
                                          ((DateTime.Now.Subtract(s.StartDate).Days <= (pd.CycleLength * pd.CycleRepeatCount))
                                            &&
                                          ((DateTime.Now.Subtract(s.StartDate).Days % pd.CycleLength) == pa.Priority))
                                        )
                                  select new TodayActivity { ProfileActivity = pa, Activity = act, Subscription = s, ProfileDetail = pd, Program = p };
                               
                ////this prevents System.ObjectDisposedException
                return queryResults.ToList();         
            }
        }
            catch (Exception)
            {
                return new List<TodayActivity>();
            }
        }

        public static IEnumerable<Program> loadUserPrograms(Guid uid)
        {
            using (var db = new iWorkoutEDMContainer())
            {
                var query = db.Programs.Where(p => p.UserOwnerKey.Id == uid);
                return query.ToList();
            }
        }

        public static IEnumerable<Subscription> loadUserSubscriptions(Guid uid)
        {
            using (var db = new iWorkoutEDMContainer())
            {
                var query = db.Subscriptions.Include("ProgramKey")
                    .Where(s => s.UserClientKey.Id == uid && s.IsActive == true);
                return query.ToList();
            }
        }

        public static List<ProgramUserAttribute> loadUserAllPrograms(Guid uid)
        {

            using (var db = new iWorkoutEDMContainer())
            {
                IEnumerable<Program> userPrograms = loadUserPrograms(uid);
                IEnumerable<Subscription> userSubscriptions = loadUserSubscriptions(uid);
                Dictionary<DBid, ProgramUserAttribute> result =
                    new Dictionary<DBid, ProgramUserAttribute>();
                foreach (var prog in userPrograms)
                {
                    result.Add(prog.Id, new ProgramUserAttribute() { Program = prog, isOwned = true, isSubscribed = false });
                }

                foreach (var subs in userSubscriptions)
                {
                    if (result.ContainsKey(subs.ProgramKey.Id))
                    {
                        result[subs.ProgramKey.Id].isSubscribed = true;
                    }
                    else
                    {
                        result.Add(subs.ProgramKey.Id, new ProgramUserAttribute() { Program = subs.ProgramKey, isOwned = false, isSubscribed = true });
                    }
                }
                return result.Values.ToList(); 
            }
        }

        public static IEnumerable<ProfileDetail> loadUserProfiles(Guid uid)
        {
            using (var db = new iWorkoutEDMContainer())
            {
                var query = db.ProfileDetails.Where(pd => pd.UserOwnerKey.Id == uid);
                return query.ToList();
            }
        }

        public static bool createProgram(Guid uid, string programName, IEnumerable<int> orderedProfiles)
        {
            try
            {
                using (var db = new iWorkoutEDMContainer())
                {
                    User user = db.Users.Find(uid);
                    Program newProg = new Program()
                    {
                        Name = programName
                    };
                    user.Programs.Add(newProg);
                    db.SaveChanges();

                    int progId = newProg.Id;
                    foreach (int profId in orderedProfiles)
                    {
                        ProfileDetail profDetail = db.ProfileDetails.Find(profId);
                        ProgramProfile newProgProf = new ProgramProfile();
                        newProg.ProgramProfiles.Add(newProgProf);
                        profDetail.ProgramProfiles.Add(newProgProf);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool createActivity(Guid uid, string description, int activityType)
        {
            try
            {
                using (var db = new iWorkoutEDMContainer())
                {
                    User user = db.Users.Find(uid);
                    ActivityType actType = db.ActivityTypes.Find(activityType);
                    Activity newActivity = new Activity() { Description = description };

                    user.Activities.Add(newActivity);
                    actType.Activities.Add(newActivity);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool createActivityLog(DBid subId, DBid profActId, bool isAccomplished, string memo)
        {
            try
            {
                using (var db = new iWorkoutEDMContainer())
                {
                    var newLog = new ActivityLog()
                    {
                        Accomplished = isAccomplished,
                        Memo = memo,
                        Date = DateTime.Now
                    };
                    var subscription = db.Subscriptions.Find(subId);
                    var profileActivity = db.ProfileActivities.Find(profActId);
                    subscription.ActivityLogs.Add(newLog);
                    profileActivity.ActivityLogs.Add(newLog);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static bool createProfile(Guid uid, List<List<DBid>> newProfileActivities)
        {
            // TODO handle profile level priority.
            try
            {
                using (var db = new iWorkoutEDMContainer())
                {
                    ProfileDetail newProfile = new ProfileDetail();
                    User profileOwner = db.Users.Find(uid);
                    profileOwner.ProfileDetails.Add(newProfile);
                    db.SaveChanges();
                    DBid newProfileId = newProfile.Id;
                    int activityPriority = 0;
                    for (int profileDayIndex = 0; profileDayIndex < newProfileActivities.Count; profileDayIndex++) 
                    {
                        foreach (DBid activityId in newProfileActivities[profileDayIndex]) {
                            Activity currentActivity = db.Activities.Find(activityId);
                            ProfileActivity newProfileActivity = new ProfileActivity()
                            { Priority = activityPriority, IsActive = true };
                            newProfile.ProfileActivities.Add(newProfileActivity);
                            currentActivity.ProfileActivities.Add(newProfileActivity);
                        }
                        activityPriority++;
                    }
                    newProfile.CycleLength = newProfileActivities.Count;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<DBpairs> getProfileDetail(DBid profileId)
        {
             try
            {
                var result = new List<DBpairs>();
                using (var db = new iWorkoutEDMContainer())
                {
                    
                    ProfileDetail profileDetail = db.ProfileDetails.Find(profileId);
                    int cycleLength = profileDetail.CycleLength;
                    
                    for (int i = 0; i < cycleLength; i++)
                    {
                        DBpairs dbPairs = new DBpairs();
                      //  var dayAct = db.ProfileActivities.Where(pa => pa.Priority == i).ToList();
                        
                        var day = from act in db.Activities
                                  join pa in db.ProfileActivities.Where(pa => pa.ProfileDetailKey.Id == profileId && pa.Priority == i && pa.IsActive == true) 
                                     on act.Id equals pa.ActivityKey.Id
                                  select act;

                        foreach (var act in day)
                        {
                            DBpair pair = new DBpair(act.Id, act.Description);
                            dbPairs.Add(pair);
                        }
                        result.Add(dbPairs);
                    }                   
                }
                return result;
            }
            catch (Exception)
            {
                return new List<DBpairs>();
            }
        }

        public static bool createSubscription(Guid uid, DBid programId)
        {
            try
            {
                using (var db = new iWorkoutEDMContainer())
                {

                    User user = db.Users.Find(uid);
                    Program program = db.Programs.Find(programId);

                    Subscription newSubscription = new Subscription()
                    {
                        IsActive = true,
                        StartDate = DateTime.Now,
                    };

                    user.Subscriptions.Add(newSubscription);
                    program.Subscriptions.Add(newSubscription);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool deactivateSubscription(DBid subId) {
            try
            {
                using (var db = new iWorkoutEDMContainer())
                {
                    Subscription subscription = db.Subscriptions.Find(subId);
                    subscription.IsActive = false;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IEnumerable<Program> loadProgramsWithPublicity(bool isPublic)
        {
            Func<Program, bool> isPublicProgram = (p => true || isPublic);
            try
            {
                using (var db = new iWorkoutEDMContainer())
                {
                    var query = db.Programs;
                    return query.ToList().Where(isPublicProgram);
                }

            }
            catch (Exception)
            {
                return new List<Program>();
            }
        }

        public static IEnumerable<User> loadCoachCustomers(Guid uid)
        {
            try
            {
                using (var db = new iWorkoutEDMContainer())
                {
                    var customers = from cr in db.CustomerRelationships.Include("UserClientKey").Include("UserCoachKey")
                                    where cr.UserCoachKey.Id == uid
                                    select cr.UserClientKey;
                    return customers.ToList();
                }
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }
    
        public static IEnumerable<UserSubscription> loadClientSubscriptions(Guid uid)
        {
              try
            {
                using (var db = new iWorkoutEDMContainer())
                {
                    User user = db.Users.Find(uid);
                    var queryResult = from sub in user.Subscriptions.Where(s => s.IsActive == true)
                                      join prog in db.Programs on sub.ProgramKey.Id equals prog.Id
                                      select new UserSubscription{ subscription = sub, program = prog};
                    return queryResult.ToList();
                }
            }
            catch (Exception)
            {
                return new List<UserSubscription>();
            }
        }


        public static bool createCustomerRelationship(Guid coachId, Guid clientId)
        {
            try
            {
                using (var db = new iWorkoutEDMContainer())
                {
                    var userCoach = db.Users.Find(coachId);
                    var userClient = db.Users.Find(clientId);
                    var newRelationship = new CustomerRelationship() { SelfCoached = false };

                    userCoach.CustomerRelationships1.Add(newRelationship);
                    userClient.CustomerRelationships.Add(newRelationship);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IEnumerable<ClientActivityLog> loadActivityLog(Guid uid)
        {
            try
            {
                using (var db = new iWorkoutEDMContainer())
                {
                    User coach = db.Users.Find(uid);
                    //IEnumerable<CustomerRelationship> cr = coach.CustomerRelationships1;
                    var query = from cr in coach.CustomerRelationships1
                                join cli in db.Users on cr.UserClientKey.Id equals cli.Id
                                join subs in db.Subscriptions on cli.Id equals subs.UserClientKey.Id
                                join al in db.ActivityLogs on subs.Id equals al.SubscriptionKey.Id
                                join pa in db.ProfileActivities on al.ProfileActivityKey.Id equals pa.Id
                                join act in db.Activities on pa.ActivityKey.Id equals act.Id
                                select new ClientActivityLog() { 
                                    ActivityLog = al,  
                                    ActivityDescription = act.Description,
                                    UserName = cli.Handle,
                                };
                    return query.ToList();
                }
            }
            catch (Exception)
            {
                return new List<ClientActivityLog>();
            }

        }
    }
}
