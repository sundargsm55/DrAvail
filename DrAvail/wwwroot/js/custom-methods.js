$(document).ready(function () {
    $(".toast").toast();

    var MHours = ["00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14"];
    var Times = ["00", "15", "30", "45"];
    var EHours = ["14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "00"];
    var count = 0;
    var Degree = ["", "", "", "", "", ""];

    //setting value to availabilityType
    $("#availabilityType").val("Common");
    SetSelectFromDegreeOnReload();
    OnReload();

    var today = new Date();
    var year = today.getFullYear();
    var month = today.getMonth();
    var day = today.getDay();
    $('#dob').datepicker({
        minDate: new Date(year - 125, month, day),
        maxDate: new Date(year - 25, month, day),
        setDate: new Date(year - 25, month, day)
    });

    $("#dob").on('change', function (event) {
        var dob = event.currentTarget.value.split("/")[2];
        $("#age").val(year - parseInt(dob));
    });

    function CheckRegistrationNumberExists(registrationNumber) {
        if (registrationNumber != null && registrationNumber.length > 5) {
            var url = window.location.origin + "/Doctors/DoctorExistsByRegistrationNumber";
            $.getJSON(url, { registrationNumber: registrationNumber, id: $('#txtID').val() }, function (data) {
                if (data == true) {
                    //console.log("Doctor already exists for Registration Number: " + registrationNumber);
                    alert("Doctor already exists for Registration Number: " + registrationNumber + ". \nPlease enter valid Registration Number");
                    return true;
                }
                //console.log("Reg Num func: ");
                //console.log(data);
            });
        }
        return false;
    }
    $("#txtRegistrationNumber").on('change', function (event) {
        var registrationNumber = event.currentTarget.value;
        CheckRegistrationNumberExists(registrationNumber);
    });

    function OnReload() {
        //console.log("Inside window on event call")
        $("#txtPincode").trigger('keyup');
        $("#txtHospitalPincode").trigger('keyup');

        var id = parseInt($("#selHospitalId").find(":selected").val());
        if (id != 0) {
            $("#selHospitalId").trigger('change');
        }


        var n = parseInt($("#chkAvailableOnWeekend:checked").length);
        if (n != 0) {
            $("#commonWeekendInput").collapse('show');
            if (CheckWeekendSameAsCommonDays()) {
                //CopyCommonToWeekend();
                toggleWeekendTiming(true);
            }
        }

        if ($('#chkAddCurrentAvailability:checked').length != 0) {
            $("#addCurrentAvailability").toggle();
            $('#showCurrentAvailability').toggle();
        }
        $("#status").trigger('click');

    }

    function SetTimeFromHourMinute(target) {
        if (target != null && target.endsWith("Time")) {
            var Hour = $(target.replace("Time", "Hour")).find(":selected").val();
            var Minute = $(target.replace("Time", "Minute")).find(":selected").val();
            $(target).val(Hour + ":" + Minute);
            //console.log("commonMorningStartTime: " + $(target).val());

        }
    }

    function setDefaultWeekendTiming(timing = "00:00") {
        //Morning start time
        $("#weekendMorningStartTime").val(timing);
        //morning end time
        $("#weekendMorningEndTime").val(timing);
        //evening start time
        $("#weekendEveningStartTime").val(timing);
        //evening end time
        $("#weekendEveningEndTime").val(timing);
    }

    $('#btnSave').on('click', function (event) {
        var IsExist = CheckRegistrationNumberExists($("#txtRegistrationNumber").val());

        if (!IsExist && ValidateExperience()) {
            //for common days
            //setCommonMorningStartTime();
            //console.log("CheckRegistrationNumberExists: " + !(IsExist));
            console.log("Inside if part CheckRegistrationNumberExists");
            SetTimeFromHourMinute("#commonMorningStartTime");
            SetTimeFromHourMinute("#commonMorningEndTime");
            SetTimeFromHourMinute("#commonEveningStartTime");
            SetTimeFromHourMinute("#commonEveningEndTime");

            //for weekends
            var availableOnWeekend = $('#chkAvailableOnWeekend:checked').length
            if (availableOnWeekend != 0) {
                SetTimeFromHourMinute("#weekendMorningStartTime");
                SetTimeFromHourMinute("#weekendMorningEndTime");
                SetTimeFromHourMinute("#weekendEveningStartTime");
                SetTimeFromHourMinute("#weekendEveningEndTime");
            }
            else {
                setDefaultWeekendTiming();
            }
            $('#myForm').submit(); //submit the form
        }
        else {
            //now it will submit regardless
            //but need to prevent from submit if above check fails
            //console.log("else CheckRegistrationNumberExists");

            event.preventDefault();
            return;
        }

    });

    function getHours(id) {
        if (id.includes("Morning")) {
            return MHours;
        } else {
            return EHours;
        }
    }

    function getFirstTiming() {
        return "<option value='" + Times[0] + "'>" + Times[0] + "</option>";
    }
    function getTimings(Tindex) {
        let timings = "";

        $.each(Times, function (i, value) {
            if (i > Tindex) {
                timings += "<option value='" + value + "'>" + value + "</option>";
            }
        });
        return timings;
    }

    function StartHour(event) {
        let Hours = getHours(event.currentTarget.id);

        let prefix = "#" + event.currentTarget.id.replace("StartHour", "");

        let startHour = $(event.currentTarget).find(":selected").val();
        let startminute = $(prefix + "StartMinute").find(":selected").val();

        if (jQuery.inArray(startHour, Hours) != -1) {
            let Hindex = Hours.indexOf(startHour);
            let items = "";
            $.each(Hours, function (i, value) {
                if (i >= Hindex) {
                    items += "<option value='" + value + "'>" + value + "</option>";
                }
            });
            $(prefix + "EndHour").html(items);
            $(prefix + "EndMinute").html(getTimings(Times.indexOf(startminute)));
        }
    }

    function EndHour(event) {
        let Hours = getHours(event.currentTarget.id);

        // current event - EndHour
        let prefix = "#" + event.currentTarget.id.replace("EndHour", "");

        let endHour = $(event.currentTarget).find(":selected").val(); // to get end hour value
        let startHour = $(prefix + "StartHour").find(":selected").val(); // to get start hour value

        let startHourIndex = Hours.indexOf(startHour);
        let endHourIndex = Hours.indexOf(endHour);

        if (endHourIndex == (Hours.length - 1)) {
            $(prefix + "EndMinute").html(getFirstTiming());
        }
        else if (endHourIndex > startHourIndex) {
            $(prefix + "EndMinute").html(getTimings(-1)); // to get & set all timings

        }
        else {
            //var startminute = $(prefix+ "StartMinute").find(":selected").val();
            //var Tindex = Times.indexOf($(prefix + "StartMinute").find(":selected").val());
            // to get timings after startMinute
            $(prefix + "EndMinute").html(getTimings(Times.indexOf($(prefix + "StartMinute").find(":selected").val())));
        }

    }

    function StartMinute(event) {

        let Hours = getHours(event.currentTarget.id);

        // current event - EndHour
        let prefix = "#" + event.currentTarget.id.replace("StartMinute", "");

        var startMinute = $(event.currentTarget).find(":selected").val();
        var startHour = $(prefix + "StartHour").find(":selected").val();
        var endHour = $(prefix + "EndHour").find(":selected").val();
        var startHourIndex = Hours.indexOf(startHour);
        var endHourIndex = Hours.indexOf(endHour);
        var Tindex = Times.indexOf(startMinute);
        var items = "";

        if (Tindex != Times.length - 1) {
            //if user previously selected 45, it should have incremented the endhour so if user changes minute back
            //end hour should match with starthour
            if (endHourIndex > startHourIndex) {
                //var Hindex = MHours.indexOf(startHour);
                //var items = "";
                $.each(Hours, function (i, value) {
                    if (i >= startHourIndex) {
                        items += "<option value='" + value + "'>" + value + "</option>";
                    }
                });
                $(prefix + "EndHour").html(items);
            }
            $(prefix + "EndMinute").html(getTimings(Tindex));
        }
        else {
            //var timings = "";
            if (startHourIndex == Hours.length - 2) {
                $(prefix + "EndMinute").html(getFirstTiming());
            }
            else {
                $(prefix + "EndMinute").html(getTimings(-1));
            }

            if (jQuery.inArray(startHour, Hours) != -1) {
                $.each(Hours, function (i, value) {
                    if (i > startHourIndex) {
                        items += "<option value='" + value + "'>" + value + "</option>";
                    }
                });
                $(prefix + "EndHour").html(items);
            }
        }
    }
    //commonMorningStartHour
    $("#commonMorningStartHour").on("change", function (event) {
        //debugger;
        StartHour(event);
    });

    //commonMorningStartMinute
    $("#commonMorningStartMinute").change(function (event) {
        var startMinute = $("#commonMorningStartMinute").find(":selected").val();
        var startHour = $("#commonMorningStartHour").find(":selected").val();
        var endHour = $("#commonMorningEndHour").find(":selected").val();
        var Hindex = MHours.indexOf(startHour);
        var endHourIndex = MHours.indexOf(endHour);
        var Tindex = Times.indexOf(startMinute);
        var timings = "";
        var items = "";

        if (Tindex != Times.length - 1) {
            $.each(Times, function (i, value) {
                if (i > Tindex) {
                    timings += "<option value='" + value + "'>" + value + "</option>";
                }
            });
            //if user previously selected 45, it should have incremented the endhour so if user changes minute back
            //end hour should match with starthour
            if (endHourIndex > Hindex) {
                //var Hindex = MHours.indexOf(startHour);
                //var items = "";
                $.each(MHours, function (i, value) {
                    if (i >= Hindex) {
                        items += "<option value='" + value + "'>" + value + "</option>";
                    }
                });
                $("#commonMorningEndHour").html(items);
            }
            $("#commonMorningEndMinute").html(timings);
        }
        else {
            //var timings = "";
            if (Hindex == MHours.length - 2) {
                timings += "<option value='" + Times[0] + "'>" + Times[0] + "</option>";
                console.log("timing::  " + timings);
            }
            else {
                $.each(Times, function (i, value) {
                    timings += "<option value='" + value + "'>" + value + "</option>";
                });
            }

            if (jQuery.inArray(startHour, MHours) != -1) {
                $.each(MHours, function (i, value) {
                    if (i > Hindex) {
                        items += "<option value='" + value + "'>" + value + "</option>";
                    }
                });
                $("#commonMorningEndHour").html(items);
                $("#commonMorningEndMinute").html(timings);
            }
        }
    });

    //commonMorningEndHour
    $("#commonMorningEndHour").on("change", function (event) {
        EndHour(event);
    });

    $("#commonMorningEndMinute").on('focus', function () {
        $("#commonMorningEndHour").trigger('change');
    });
    //commonEveningStartHour
    $("#commonEveningStartHour").on("change", function (event) {
        //debugger;
        StartHour(event);
    });

    //commonEveningStartMinute
    $("#commonEveningStartMinute").change(function () {
        var startminute = $("#commonEveningStartMinute").find(":selected").val();
        var startHour = $("#commonEveningStartHour").find(":selected").val();
        var endHour = $("#commonEveningEndHour").find(":selected").val();
        var Tindex = Times.indexOf(startminute);
        var Hindex = EHours.indexOf(startHour);
        var endHourIndex = EHours.indexOf(endHour);
        var timings = "";
        var items = "";

        if (Hindex == EHours.length - 2 && Tindex == Times.length - 1) {
            timings += "<option value='" + Times[0] + "'>" + Times[0] + "</option>";
            items += "<option value='" + EHours[EHours.length - 1] + "'>" + EHours[EHours.length - 1] + "</option>";
            $("#commonEveningEndHour").html(items);
            $("#commonEveningEndMinute").html(timings);
        }
        else if (Tindex != Times.length - 1) {
            $.each(Times, function (i, value) {
                if (i > Tindex) {
                    timings += "<option value='" + value + "'>" + value + "</option>";
                }
            });
            if (endHourIndex > Hindex) {
                //var Hindex = EHours.indexOf(startHour);
                $.each(EHours, function (i, value) {
                    if (i >= Hindex) {
                        items += "<option value='" + value + "'>" + value + "</option>";
                    }
                });
                $("#commonEveningEndHour").html(items);
            }
            $("#commonEveningEndMinute").html(timings);
        }
        else {
            //var timings = "";
            $.each(Times, function (i, value) {
                timings += "<option value='" + value + "'>" + value + "</option>";
            });
            if (jQuery.inArray(startHour, EHours) != -1) {
                //var items = "";
                $.each(EHours, function (i, value) {
                    if (i > Hindex) {
                        items += "<option value='" + value + "'>" + value + "</option>";
                    }
                });
                $("#commonEveningEndHour").html(items);
                $("#commonEveningEndMinute").html(timings);
            }
        }
    });

    //commonEveningEndHour
    $("#commonEveningEndHour").on("change", function (event) {
        EndHour(event);
    });

    //weekends
    //weekendMorningStartHour
    $("#weekendMorningStartHour").on("change", function (event) {
        //debugger;
        StartHour(event);
    });

    //weekendMorningStartMinute
    $("#weekendMorningStartMinute").change(function () {
        var startMinute = $("#weekendMorningStartMinute").find(":selected").val();
        var startHour = $("#weekendMorningStartHour").find(":selected").val();
        var endHour = $("#weekendMorningEndHour").find(":selected").val();
        var Hindex = MHours.indexOf(startHour);
        var endHourIndex = MHours.indexOf(endHour);
        var Tindex = Times.indexOf(startMinute);
        if (Tindex != Times.length - 1) {
            var timings = "";
            $.each(Times, function (i, value) {
                if (i > Tindex) {
                    timings += "<option value='" + value + "'>" + value + "</option>";
                }
            });
            //if user previously selected 45, it should have incremented the endhour so if user changes minute back
            //end hour should match with starthour
            if (endHourIndex > Hindex) {
                var items = "";
                $.each(MHours, function (i, value) {
                    if (i >= Hindex) {
                        items += "<option value='" + value + "'>" + value + "</option>";
                    }
                });
                $("#weekendMorningEndHour").html(items);
            }
            $("#weekendMorningEndMinute").html(timings);
        }
        else {
            var timings = "";
            if (Hindex == MHours.length - 2) {
                timings += "<option value='" + Times[0] + "'>" + Times[0] + "</option>";
                console.log("timing::  " + timings);
            }
            else {
                $.each(Times, function (i, value) {
                    timings += "<option value='" + value + "'>" + value + "</option>";
                });
            }
            if (jQuery.inArray(startHour, MHours) != -1) {
                var Hindex = MHours.indexOf(startHour);
                var items = "";
                $.each(MHours, function (i, value) {
                    if (i > Hindex) {
                        items += "<option value='" + value + "'>" + value + "</option>";
                    }
                });
                $("#weekendMorningEndHour").html(items);
                $("#weekendMorningEndMinute").html(timings);
            }
        }
    });

    //weekendMorningEndHour
    $("#weekendMorningEndHour").on("change", function (event) {
        EndHour(event);
    });

    //weekendEveningStartHour
    $("#weekendEveningStartHour").on("change", function (event) {
        //debugger;
        StartHour(event);
    });

    //weekendEveningStartMinute
    $("#weekendEveningStartMinute").change(function () {
        var startminute = $("#weekendEveningStartMinute").find(":selected").val();
        var startHour = $("#weekendEveningStartHour").find(":selected").val();
        var endHour = $("#weekendEveningEndHour").find(":selected").val();
        var Tindex = Times.indexOf(startminute);
        var Hindex = EHours.indexOf(startHour);
        var endHourIndex = EHours.indexOf(endHour);
        var timings = "";
        var items = "";

        if (Hindex == EHours.length - 2 && Tindex == Times.length - 1) {
            timings += "<option value='" + Times[0] + "'>" + Times[0] + "</option>";
            items += "<option value='" + EHours[EHours.length - 1] + "'>" + EHours[EHours.length - 1] + "</option>";
            $("#weekendEveningEndHour").html(items);
            $("#weekendEveningEndMinute").html(timings);
        }
        else if (Tindex != Times.length - 1) {
            $.each(Times, function (i, value) {
                if (i > Tindex) {
                    timings += "<option value='" + value + "'>" + value + "</option>";
                }
            });
            if (endHourIndex > Hindex) {
                //var Hindex = EHours.indexOf(startHour);
                $.each(EHours, function (i, value) {
                    if (i >= Hindex) {
                        items += "<option value='" + value + "'>" + value + "</option>";
                    }
                });
                $("#weekendEveningEndHour").html(items);
            }
            $("#weekendEveningEndMinute").html(timings);
        }
        else {
            //var timings = "";
            $.each(Times, function (i, value) {
                timings += "<option value='" + value + "'>" + value + "</option>";
            });
            if (jQuery.inArray(startHour, MHours) != -1) {
                //var items = "";
                $.each(EHours, function (i, value) {
                    if (i > Hindex) {
                        items += "<option value='" + value + "'>" + value + "</option>";
                    }
                });
                $("#weekendEveningEndHour").html(items);
                $("#weekendEveningEndMinute").html(timings);
            }
        }
    });

    //weekendEveningEndHour
    $("#weekendEveningEndHour").on("change", function (event) {
        EndHour(event);
    });

    function CheckWeekendSameAsCommonDays() {
        if ($("#sameAsCommonDays:checked").length != 0) {
            return true;
        }
        return false;
    }

    function CopyToWeekend(source) {
        var target = source.replace("common", "weekend");
        $(target).val($(source).find(":selected").val()).trigger('change');
    }

    function CopyCommonToWeekend() {

        CopyToWeekend("#commonMorningStartHour");
        CopyToWeekend("#commonMorningStartMinute");
        CopyToWeekend("#commonMorningEndHour");
        CopyToWeekend("#commonMorningEndMinute");

        CopyToWeekend("#commonEveningStartHour");
        CopyToWeekend("#commonEveningStartMinute");
        CopyToWeekend("#commonEveningEndHour");
        CopyToWeekend("#commonEveningEndMinute");
    }

    $("#sameAsCommonDays").click(function () {
        if (CheckWeekendSameAsCommonDays()) {
            CopyCommonToWeekend();
            toggleWeekendTiming(true);
        }
        else {
            toggleWeekendTiming(false);
        }
    });

    function disableAttribute(source, status) {
        $(source).attr("disabled", status);
    }

    function toggleWeekendTiming(status) {

        disableAttribute("#weekendMorningStartHour", status);
        disableAttribute("#weekendMorningEndHour", status);
        disableAttribute("#weekendMorningStartMinute", status);
        disableAttribute("#weekendMorningEndMinute", status);

        disableAttribute("#weekendEveningStartHour", status);
        disableAttribute("#weekendEveningEndHour", status);
        disableAttribute("#weekendEveningStartMinute", status);
        disableAttribute("#weekendEveningEndMinute", status);
    }

    function ValidateExperience() {
        let exp = $("#txtExperience").val();
        let dob = $("#dob").val();
        if (dob !== "") {
            let age = new Date().getFullYear() - new Date(dob).getFullYear();
            console.log("\nAge: " + age);
            if (age > 24) {
                let diff = age - 25;
                if (exp >= 0 && exp <= diff) {
                    //alert('ok');
                    return true;
                }
                else {
                    alert("You can't have " + exp + " of experience when you are " + age + " year old");
                }
            }
            else {
                alert("Age should be at 25");
            }
        }
        else {
            alert("Enter Date of Birth");
        }
        return false;
    }

    $("#txtExperience").on('change', function () {
        //debugger;
        ValidateExperience();
    });

    //for doctor
    $("#txtPincode").on('keyup', function () {
        //debugger;
        //var source = "#txtPincode";
        //console.log("Pincode: " + $("#txtPincode").val());
        if ($("#txtPincode").val().length == 6) {
            var url = window.location.origin + "/Doctors/GetLocations";
            $.getJSON(url, { Pincode: $("#txtPincode").val() }, function (data) {
                if (data.length == 0) {
                    console.log("No location found!!!");
                    return false;
                }
                var items = "";
                //console.log($("#City").find(":selected").val());
                //$("#City").empty();
                $("#txtDistrict").val(data[0].district);
                //$("txtDistrict").text = data[0].dis;
                //console.log(data[0].dis);
                $.each(data, function (i, location) {
                    items += "<option value='" + location.city + "'>" + location.city + "</option>";
                });
                $("#City").html(items);
            });
        }
    });
    //for hostpital
    $("#txtHospitalPincode").on('keyup', function () {
        //debugger;
        //var source = "#txtPincode";
        //console.log("Pincode: " + $("#txtPincode").val());
        if ($("#txtHospitalPincode").val().length == 6) {
            var url = window.location.origin + "/Doctors/GetLocations";
            $.getJSON(url, { Pincode: $("#txtHospitalPincode").val() }, function (data) {
                if (data.length == 0) {
                    console.log("No location found!!!");
                    return false;
                }
                var items = "";
                //$("#HospitalCity").empty();
                $("#txtHospitalDistrict").val(data[0].district);
                //$("txtDistrict").text = data[0].dis;
                //console.log(data[0].dis);
                $.each(data, function (i, location) {
                    items += "<option value='" + location.city + "'>" + location.city + "</option>";
                });
                $("#HospitalCity").html(items);
            });
        }
    });

    //City select list
    //on mousedown event
    $("select").on('mousedown', function (event) {
        var source = event.currentTarget;
        //if (source.id == "Speciality") return;
        if (!source.id.includes("common")) return;
        if (!source.id.includes("weekend")) return;

        //console.log("Option lenght: " + $(source).find("option").length);
        if ($(source).attr("size") == '1' && $(source).find("option").length > 5) {
            event.preventDefault();
        }
    });
    //on click event
    $("select").on('click', function (event) {
        var source = event.currentTarget;
        if (!source.id.includes("common") && !source.id.includes("weekend")) {
            console.log("Click event of not common or weekend")
            return;
        }
        console.log("Click event of common or weekend field")

        //console.log($(source).attr("size"));
        if ($(source).attr("size") == '1') {
            if ($(source).find("option").length > 5) {
                $(source).addClass("selectOpen");
                $(source).attr("size", '5');
            }
        }
        else {
            if (CheckWeekendSameAsCommonDays()) {
                if (source.id.startsWith("common")) {
                    CopyToWeekend("#" + source.id);
                }
            }
            $(source).removeClass("selectOpen");
            $(source).attr("size", '1');
        }
    });

    //restting when loses focus
    $("select").on('focusout', function (event) {
        var source = event.currentTarget;
        //if (source.id == "Speciality") return;
        if (!source.id.includes("common")) return;
        if (!source.id.includes("weekend")) return;

        //Should be simplied to update the field on which change occurs
        //example: if CommonMorningEndHour is changed, then change weekendMorningEndHour only
        if (CheckWeekendSameAsCommonDays()) {
            if (source.id.startsWith("common")) {
                CopyToWeekend("#" + source.id);
            }
        }
        $(source).removeClass("selectOpen");
        $(source).attr("size", '1');
    });


    $("select").on('mouseleave', function (event) {
        var source = event.currentTarget;
        //if (source.id == "Speciality") return;
        if (!source.id.includes("common")) return;
        if (!source.id.includes("weekend")) return;


        $(source).removeClass("selectOpen");
        $(source).attr("size", '1');
    });

    $('#selHospitalId').one('change click', function () {
        var choice = parseInt($("#selHospitalId").find(":selected").val());
        if (choice != 0) {
            var url = window.location.origin + "/Hospitals/FetchHospitalDetails";
            $.getJSON(url, { id: choice }, function (data) {
                if (data.length != 0) {
                    if (data == false) {
                        console.log("No Hospital found!!!");
                        return false;
                    }
                    else {
                        $("#txtHospitalName").val(data.name);
                        $("#txtHospitalType").val(data.type);
                        $("#txtHospitalAddress").val(data.address);
                        $("#txtHospitalPincode").val(data.pincode).trigger('keyup');
                        //$("#txtHospitalDistrict").val(data.district);
                        $("#HospitalCity").val(data.city);
                        console.log("Hospital City: " + $("#HospitalCity").val());
                        $("#txtHospitalEmail").val(data.email);
                        $("#txtHospitalPhone").val(data.phone);
                    }
                }
            });
        }
    });

    $("#status").on('click', function () {
        console.log($("#status").find(":selected").val());
        if ($("#status").find(":selected").val() == "Available") {
            $("#currentHospital").show();
            console.log("Show : " + $("#status").find(":selected").val());

        }
        else {
            $("#currentHospital").hide();
            console.log("Hide : " + $("#status").find(":selected").val());
        }
    });

    $('#btnAddCurrentAvailability').on('click', function () {
        //$("#addCurrentAvailability").toggle();
        $('#chkAddCurrentAvailability').each(function () { this.checked = !this.checked; });
    });

    $('#btnRemoveCurrentAvailability').on('click', function () {
        //$("#addCurrentAvailability").toggle();
        $('#chkAddCurrentAvailability').each(function () { this.checked = !this.checked; });
    });

    function SetDegree() {
        var value = "";
        $.each(Degree, function (i, v) {
            if (v != "") {
                if (i == 0) {
                    value = v;
                }
                else {
                    value = value + ", " + v;
                }
            }
            else {
                return false;
            }
        });
        //$("#txtDegree").removeAttr('value');
        $("#txtDegree").val(value).trigger('keyup');
        //$("#txtDegree").val(value);

        $("#txtDegree").attr("value", value).trigger('keyup');
    }

    $(".divDegree").on('change', "select.education", function (event) {
        //console.log("trigged change for: " + event.target.id);
        //var value = $("#txtDegree").val();
        var lastChar = event.target.id.charAt(event.target.id.length - 1);
        if (lastChar == "t") {
            Degree[0] = $("#" + event.target.id).val();
        }
        else {
            Degree[parseInt(lastChar)] = $("#" + event.target.id).val();
        }

        SetDegree();

    });

    $("#btnAddDegree").on('click', function () {
        count++;
        //console.log("Counter: " + count);
        if (count == 1) {
            $("#divDegree1").toggle();
        }
        else if (count < 5) {
            //$("#txtDegree").val($("#txtDegree").val() + $("#txtDegree" + count).val());
            var item = $("#divDegree1").html().replaceAll("Degree1", "Degree" + count);
            $("#divCardDegree").append(item);
        }
        else {
            alert("You can only add 5 Degree's!");
        }

    });

    $(".divDegree").on('click', "a.removebutton", function (event) {

        var lastChar = event.target.id.charAt(event.target.id.length - 1);
        Degree[parseInt(lastChar)] = "";

        SetDegree();

        console.log("Remove Counter: " + count);

        if (count == 1) {
            $("#divDegree1").toggle();
        } else {
            $(this).parent().remove();
        }
        count--;

    });

    function SetSelectFromDegreeOnReload() {
        console.log("inside SetSelectFromDegreeOnReload");
        var degreeArray = $("#txtDegree").val().split(", ");
        console.log("degreeArray: " + degreeArray);
        $.each(degreeArray, function (i, degree) {
            if (i == 0) {
                $("#txtDegreeDefault").val(degree);
            }
            else {
                $("#btnAddDegree").trigger('click'); //to add another degree
                $("#txtDegree" + i).val(degree);
            }
            Degree[i] = degree;
        });
    }
});