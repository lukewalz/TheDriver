<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TheDriver.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron text-center">
        <img src="Icons/driverlogo.png" />
    </div>

    <!-- container (about section) -->

    <div id="sub" class="container-fluid text-center">
        <h1>let us drive you</h1>
        <a href="#about">
            <div class="glyphicon glyphicon-chevron-down"></div>
        </a>
    </div>

    <div id="about" class="container-fluid text-center">
        <br />
        <br />
        <div class="row">
            <div class="col-sm-12">
                <h4>we are a locally owned, small business that focuses on your needs.  we specialize in personal and corporate services. whether you need a driver for days, evenings, or special events, we are the company for you.<br>
                    <br>
                    our driver arrives at the requested time, dressed in professional attire, ready to chauffeur you wherever you want to go.<br>
                    <br />
                    <strong>when you're ready, we’re ready.</strong><br>
                    <br>
                    we'll provide a driver to drive you to work, entertain, or rest &amp; relax in the back seat. 
                </h4>
                <br>
            </div>
        </div>
    </div>

    <div id="why" class="container-fluid bg-grey">
        <div class="row">
            <div class="col-sm-4">
                <span class="glyphicon glyphicon-map-marker logo slideanim slide"></span>
            </div>
            <div class="col-sm-8">
                <h2>why us</h2>
                <br>
                <h4>there are many reasons to hire a personal driver:</h4>
                you’ll double your productivity, travel more safely, and find you’re more focused, prepared and relaxed throughout your day of business meetings, personal appointments, community events and social activities.<br>
                <br>
                why not use an uber or lyft? they lack the personal attention to detail our drivers are known for providing.  there is nothing worse than being stuck in a cab or car with the wrong driver.  you won’t make that mistake with us.
                    <br>
            </div>
        </div>
    </div>

    <!-- container (services section) -->
    <div id="services" class="container-fluid text-center">
        <h2>services</h2>
        <h4>what we offer</h4>
        <br>
        <div class="row slideanim slide">
            <div class="col-sm-3">
                <span class="glyphicon glyphicon-plane logo-small"></span>
                <h4>airport services</h4>
            </div>
            <div class="col-sm-3">
                <span class="glyphicon glyphicon-star logo-small"></span>
                <h4>personal chauffeur</h4>
            </div>
            <div class="col-sm-3">
                <span class="glyphicon glyphicon-briefcase logo-small"></span>
                <h4>corporate &amp; fulltime</h4>
            </div>
            <div class="col-sm-3">
                <span class="glyphicon glyphicon-wrench logo-small"></span>
                <h4>tradeshows</h4>
            </div>
        </div>
        <br>
        <br>
        <div class="row slideanim slide">
            <div class="col-sm-3">
                <span class="glyphicon glyphicon-calendar logo-small"></span>
                <h4>large group events</h4>
            </div>
            <div class="col-sm-3">
                <span class="glyphicon glyphicon-plus logo-small"></span>
                <h4>doctor's visits</h4>
            </div>
            <div class="col-sm-3">
                <span class="glyphicon glyphicon-heart logo-small"></span>
                <h4>weddings</h4>
            </div>
            <div class="col-sm-3">
                <span class="glyphicon glyphicon-shopping-cart logo-small"></span>
                <h4>errand running</h4>
            </div>
        </div>
    </div>
    <div id="schedule">
        <!-- container (portfolio section) -->
        <div id="scheduleDeny" class="container-fluid bg-grey" runat="server">
            <h2 class="text-center">schedule service</h2>
            <h4 class="text-center">need a ride? just select a day, time, and give us your address. that easy.</h4>
            <h5 class="text-center">if time does not show up, it is most likely taken but you can email us to make sure</h5>
            <div id="schmes" style="text-align: center; font-weight: 900">in order to schedule a pickup, you must first <a href="login.aspx">login</a> or <a href="register.aspx">register</a></div>
        </div>

        <div id="schedulAccept" class="container-fluid bg-grey" runat="server">
            <h2 class="text-center">schedule service</h2>
            <form runat="server" id="scheduler">
                <label for="startdate">start date: <asp:Calendar runat="server" id="startdate" OnSelectionChanged="startdate_SelectionChanged"></asp:Calendar><br />
                <label for="starttime">start time: <asp:DropDownList runat="server" id="starttime" cssclass="bootstrap dropdown" OnSelectedIndexChanged="starttime_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br />
                <label for="endtime">end time: <asp:DropDownList id="endtime" runat="server" CssClass="bootstrap dropdown" /><br />
                <label for="location">pickup location: </label><input id="location" runat="server" type="text" required="required" /><br />
                <label for="service">service: </label><select id="service" runat="server" required="required" class="dropdown">
                    <option value="selectservice" selected="selected" disabled="disabled">select a service</option>
                    <option value="airport">airport services</option>
                    <option value="chauffeur">personal chauffeur</option>
                    <option value="corporate">corporate</option>
                    <option value="tradeshow">tradeshow</option>
                    <option value="group">group event</option>
                    <option value="doctor">doctor's visit</option>
                    <option value="wedding">wedding</option>
                    <option value="errand">errand running</option>
                </select><br />
                <asp:button runat="server" text="request a ride" id="requestride" />


            </form>
        </div>
    </div>

    <h1 class="text-center">Down for maintenance</h1>
    <!-- Container (Contact Section) -->
    <div id="contact">
        <div class="container-fluid" runat="server">
            <h2 class="text-center">CONTACT</h2>
            <div class="col-sm-12 text-center">
                <p>Contact us and we'll get back to you within 24 hours.</p>
                <p><span class="glyphicon glyphicon-map-marker"></span>Omaha, NE</p>
                <p><span class="glyphicon glyphicon-phone"></span>4026990099</p>
                <p><span class="glyphicon glyphicon-envelope"></span>thedriveromaha@gmail.com</p>
                <a href="Icons/contacts.vcf">
                    <div onclick="Icons/contacts.vcf" runat="server" class="btn btn-primary">
                        Add as Contact
                    </div>
                </a>
                <div runat="server" id="cook"></div>
            </div>
        </div>
    </div>

</asp:Content>

