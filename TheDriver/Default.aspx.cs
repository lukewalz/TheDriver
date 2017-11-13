using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Itenso.TimePeriod;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace TheDriver
{
    public partial class Default : System.Web.UI.Page
    {
        IList<Event> eventList = new List<Event>();

        public UserCredential credential;
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("client_secret.json");

            credential = GetCredential(path);
            if (Request.Cookies["UserSettings"] != null)
            {
                schedulAccept.Visible = true;
                scheduleDeny.Visible = false;
            }
            else
            {
                schedulAccept.Visible = false;
                scheduleDeny.Visible = true;
            }

            getData();
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
            }
        }

        public void getData()
        {
            // Create the service.
            CalendarService service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "The Driver Omaha",
            });

            eventList = service.Events.List("primary").Execute().Items;

            List<TimeSpan> timeSpanList = new List<TimeSpan>();

            foreach (Event _event in eventList)
            {
                try
                {
                    TimeSpan amountTime = (TimeSpan)(_event.End.DateTime - _event.Start.DateTime);
                }
                catch (Exception)
                {
                    continue;
                }

            }
        }

        public void fillStartTimeFields()
        {
            List<DateTime> timeList = new List<DateTime>();
            for (int i = 0; i < 24; i++)
            {
                var t = startdate.SelectedDate.Date.ToString("MM / dd / yyyy");

                timeList.Add(DateTime.Parse(t + " " + (i + ":00").ToString()));
            }

            RemoveTimes(timeList);

            starttime.DataSource = timeList;
            starttime.DataBind();
        }

        private List<DateTime> RemoveTimes(List<DateTime> timeList)
        {
            foreach (var item in eventList)
            {
                try
                {
                    if (item.Start.DateTime.Value.Date.ToString("MM/dd/yyyy") == startdate.SelectedDate.Date.ToString("MM/dd/yyyy"))
                    {


                        TimeInterval ti = new TimeInterval();
                        ti.StartInterval = (DateTime)item.Start.DateTime;
                        ti.EndInterval = (DateTime)item.End.DateTime;

                        try
                        {
                            timeList.RemoveRange(ti.Start.TimeOfDay.Hours + 1, ti.Duration.Hours);
                        }
                        catch
                        {
                            continue;
                        }
                        return timeList;

                    }
                    return timeList;
                }
                catch (Exception)
                {

                    continue;
                }

            }
            return timeList;
        }

        public void fillEndTimeField()
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

            endtimeList = RemoveTimes(endtimeList);

            endtime.DataSource = endtimeList;
            endtime.DataBind();
            starttime.DataBind();
        }

        protected void startdate_SelectionChanged(object sender, EventArgs e)
        {
            fillStartTimeFields();
        }

        protected void starttime_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillEndTimeField();
        }
    }
}