using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Itenso.TimePeriod;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;
using TextmagicRest;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TheDriver
{
    public partial class Default : System.Web.UI.Page
    {
        public User _user = new User();

        public List<int> times = new List<int>()
        {
            0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24
        };
        public UserCredential credential;
        protected void Page_Load(object sender, EventArgs e)
        {

            string path = System.Web.HttpContext.Current.Server.MapPath("client_secret.json");

            credential = GetCredential(path);
            if (Request.Cookies["UserSettings"] != null)
            {
                _user.ID = Convert.ToInt64(Request.Cookies["UserSettings"]["ID"]);
                _user.FirstName = Request.Cookies["UserSettings"]["FirstName"];
                _user.LastName = Request.Cookies["UserSettings"]["LastName"];
                _user.PhoneNumber = Request.Cookies["UserSettings"]["PhoneNumber"];
                _user.EmailAddress = Request.Cookies["UserSettings"]["EmailAddress"];

                schedulAccept.Visible = true;
                scheduleDeny.Visible = false;
            }
            else
            {
                schedulAccept.Visible = false;
                scheduleDeny.Visible = true;
            }
        }

        private UserCredential GetCredential(string path)
        {
            string[] Scopes = { CalendarService.Scope.Calendar };
            UserCredential credential;

            var ob = JsonConvert.DeserializeObject(File.ReadAllText(path));
            var folder = System.Web.HttpContext.Current.Server.MapPath("/App_Data/MyGoogleStorage");

            using (var stream =
                new FileStream(path, FileMode.Open, FileAccess.Read))
            {

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(folder, true)).Result;
                return credential;
            };
        }

        public void getData(DateTime start)
        {
            List<int> removeTimesList = new List<int>();
            // Create the service.
            CalendarService service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "The Driver Omaha",
            });

            var f = service.Events.List("primary");
            f.TimeMin = start;
            f.TimeMax = start.AddHours(24);

            Events events = f.Execute();

            foreach (Event _event in events.Items)
            {
                try
                {
                    TimeSpan amountTime = (TimeSpan)(_event.End.DateTime - _event.Start.DateTime);
                    for (int i = _event.Start.DateTime.Value.Hour; i < _event.End.DateTime.Value.Hour; i++)
                    {
                        removeTimesList.Add(i);
                    }
                }
                catch (Exception)
                {
                    continue;
                };
            };

            foreach (var item in removeTimesList)
            {
                times.Remove(item);
            }
        }

        public void FillEndTimeField()
        {
            List<DateTime> endtimeList = new List<DateTime>();
            for (int i = starttime.SelectedIndex + 1; i <= 24; i++)
            {
                var t = "";
                if (i != 24)
                {
                    t = startdate.SelectedDate.Date.ToString("MM / dd / yyyy");
                    var d = DateTime.Parse((t + " " + (i + ":00")).ToString());
                    endtimeList.Add(d);
                }
                else
                {
                    t = startdate.SelectedDate.Date.AddDays(1).ToString("MM / dd/ yyyy");
                    i = 0;
                    var d = DateTime.Parse((t + " " + (i + ":00")).ToString());
                    endtimeList.Add(d);
                    i = 25;
                }


            }

            endtime.DataSource = endtimeList;
            endtime.DataBind();
            starttime.DataBind();
        }

        protected void startdate_SelectionChanged(object sender, EventArgs e)
        {
            getData(startdate.SelectedDate);
            List<DateTime> converted = new List<DateTime>();
            foreach (int time in times)
            {
                var day = startdate.SelectedDate.Date.ToString("MM / dd / yyyy");

                var t = time.ToString() + ":00:00";
                var dtc = day + " " + t;
                try
                {
                    var r = DateTime.Parse(dtc);
                    converted.Add(r);
                }
                catch (Exception)
                {
                    day = startdate.SelectedDate.Date.AddDays(1).ToString("MM / dd / yyyy");
                    t = "0:00:00";
                    dtc = day + " " + t;
                    var y = DateTime.Parse(dtc);
                    converted.Add(y);
                }




            }

            starttime.DataSource = converted;
            starttime.DataBind();

        }

        protected void starttime_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> endt = new List<string>();
            foreach (var item in starttime.Items)
            {
                var d = DateTime.Parse(starttime.SelectedValue);
                if (DateTime.Parse(item.ToString()).Hour > Convert.ToInt32(d.Hour))
                {
                    endt.Add(item.ToString());
                }
            }

            endtime.DataSource = endt;
            endtime.DataBind();

        }



        protected void requestride_Click(object sender, EventArgs e)
        {
            CalendarService service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "The Driver Omaha",
            });

            Event newEvent = new Event()
            {
                Summary = serviceer.Value + " service for " + _user.FirstName,
                Location = location.Value,
                Description = "Phone: " + _user.PhoneNumber,
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Parse(starttime.SelectedValue),
                    TimeZone = "America/Chicago",
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Parse(endtime.SelectedValue),
                    TimeZone = "America/Chicago",
                },
                Attendees = new EventAttendee[] {
        new EventAttendee() { Email = _user.EmailAddress }
                },
                Reminders = new Event.RemindersData()
                {
                    UseDefault = false,
                    Overrides = new EventReminder[] {
            new EventReminder() { Method = "email", Minutes = 24 * 60 },
            new EventReminder() { Method = "sms", Minutes = 10 },
        }
                }
            };

            String calendarId = "primary";
            EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarId);
            Event createdEvent = request.Execute();
            SendConfirmationTest(starttime.SelectedValue);
        }

        private void SendConfirmationTest(string start)
        {
            // Your Account SID from twilio.com/console
            var accountSid = "AC1007cfc1620d9e7c3acd8e8856e531d5";
            // Your Auth Token from twilio.com/console
            var authToken = "8607fef5a39e4694f5d52b3748f81a10";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                to: new PhoneNumber(_user.PhoneNumber),
                from: new PhoneNumber("+1 531-201-6724"),
                body: "Congratulations " + _user.FirstName + "! You have successfully requested a ride from The Driver on " + start + "! Please wait for a confirmation email. We will be in contact shortly.");
        }
    }
}