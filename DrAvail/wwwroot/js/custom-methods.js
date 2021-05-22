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
        minDate: new Date(year - 120, 0, 1),
        maxDate: new Date(year - 26, 11, 31),
        setDate: new Date(year - 26, month-1, day)
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
        let startMinuteIndex = Times.indexOf(startminute);
        let startHourIndex = Hours.indexOf(startHour);
        debugger;
        let endHour = $(prefix + "EndHour").find(":selected").val(); // to get end hour value
        let endHourIndex = Hours.indexOf(endHour);

        if (endHourIndex > startHourIndex) {

            if (startMinuteIndex == (Times.length - 1)) {
                $.each(Hours, function (i, value) {
                    if (i > startHourIndex) {
                        items += "<option value='" + value + "'>" + value + "</option>";
                    }
                });
                $(prefix + "EndHour").html(items);
                $(prefix + "EndHour").val(endHour);
                return;
            }
        }
        else {
            endHour = startHour;
        }

        if (jQuery.inArray(startHour, Hours) != -1) {
            let items = "";
            $.each(Hours, function (i, value) {
                if (i >= startHourIndex) {
                    items += "<option value='" + value + "'>" + value + "</option>";
                }
            });
            $(prefix + "EndHour").html(items);
            $(prefix + "EndMinute").html(getTimings(startMinuteIndex));
        }

        $(prefix + "EndHour").val(endHour);
        $(prefix + "EndMinute").val(Times[startMinuteIndex +1]);

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

        // current event - StartMinute
        let prefix = "#" + event.currentTarget.id.replace("StartMinute", "");

        let startMinute = $(event.currentTarget).find(":selected").val();
        let startHour = $(prefix + "StartHour").find(":selected").val();
        var endHour = $(prefix + "EndHour").val();
        var endMinute = $(prefix + "EndMinute").val();

        if (endHour == null) { endHour = $(prefix + "EndHour" + " option:first").val(); }
        if (endMinute == null) { endMinute = $(prefix + "EndMinute" + " option:first").val(); }

        let startHourIndex = Hours.indexOf(startHour);
        let endHourIndex = Hours.indexOf(endHour);
        let startMinuteIndex = Times.indexOf(startMinute);
        let items = "";

        if (startMinuteIndex != Times.length - 1) {
            //if user previously selected 45, it should have incremented the endhour so if user changes minute back
            //end hour should match with starthour
            if (endHourIndex > startHourIndex) {
                $.each(Hours, function (i, value) {
                    if (i >= startHourIndex) {
                        items += "<option value='" + value + "'>" + value + "</option>";
                    }
                });
                $(prefix + "EndHour").html(items);
                $(prefix + "EndMinute").html(getTimings(-1)); 
            }
            else {
                $(prefix + "EndMinute").html(getTimings(startMinuteIndex));
                endMinute = Times[startMinuteIndex + 1];
            }
        }
        else {
            //var timings = "";
            if (startHourIndex == Hours.length - 2) {
                $(prefix + "EndMinute").html(getFirstTiming());
                return;
            }
            else {
                $(prefix + "EndMinute").html(getTimings(-1)); // to get all timings
            }

            if (jQuery.inArray(startHour, Hours) != -1) {
                $.each(Hours, function (i, value) {
                    if (i > startHourIndex) {
                        items += "<option value='" + value + "'>" + value + "</option>";
                    }
                });
                endHour = Hours[startHourIndex + 1];
                $(prefix + "EndHour").html(items);
            }
        }
        $(prefix + "EndHour").val(endHour);
        $(prefix + "EndMinute").val(endMinute);

    }

    //commonMorningStartHour
    $("#commonMorningStartHour").on("change", function (event) {
        //debugger;
        StartHour(event);
    });

    //commonMorningStartMinute
    $("#commonMorningStartMinute").on("change", function (event) {
        StartMinute(event);
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
        StartHour(event);
    });

    //commonEveningStartMinute
    $("#commonEveningStartMinute").on("change",function (event) {
        StartMinute(event);
    });

    //commonEveningEndHour
    $("#commonEveningEndHour").on("change", function (event) {
        EndHour(event);
    });

    $("#commonEveningEndMinute").on('focus', function () {
        $("#commonEveningEndHour").trigger('change');
    });
    //weekends
    //weekendMorningStartHour
    $("#weekendMorningStartHour").on("change", function (event) {
        //debugger;
        StartHour(event);
    });

    //weekendMorningStartMinute
    $("#weekendMorningStartMinute").on("change", function (event) {
        StartMinute(event);
    });

    //weekendMorningEndHour
    $("#weekendMorningEndHour").on("change", function (event) {
        EndHour(event);
    });

    $("#weekendMorningEndMinute").on('focus', function () {
        $("#weekendMorningEndHour").trigger('change');
    });

    //weekendEveningStartHour
    $("#weekendEveningStartHour").on("change", function (event) {
        StartHour(event);
    });

    //weekendEveningStartMinute
    $("#weekendEveningStartMinute").on("change",function (event) {
        StartMinute(event);
    });

    //weekendEveningEndHour
    $("#weekendEveningEndHour").on("change", function (event) {
        EndHour(event);
    });

    $("#weekendEveningEndMinute").on('focus', function () {
        $("#weekendEveningEndHour").trigger('change');
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
        ValidateExperience();
    });

    //for doctor
    $("#txtPincode").on('keyup', function () {
        if ($("#txtPincode").val().length == 6) {
            var url = window.location.origin + "/Doctors/GetLocations";
            $.getJSON(url, { Pincode: $("#txtPincode").val() }, function (data) {
                if (data.length == 0) {
                    console.log("No location found!!!");
                    return false;
                }
                var items = "";
                $("#txtDistrict").val(data[0].district);
                $.each(data, function (i, location) {
                    items += "<option value='" + location.city + "'>" + location.city + "</option>";
                });
                $("#City").html(items);
            });
        }
    });
    //for hostpital
    $("#txtHospitalPincode").on('keyup', function () {
        if ($("#txtHospitalPincode").val().length == 6) {
            var url = window.location.origin + "/Doctors/GetLocations";
            $.getJSON(url, { Pincode: $("#txtHospitalPincode").val() }, function (data) {
                if (data.length == 0) {
                    console.log("No location found!!!");
                    return false;
                }
                var items = "";
                $("#txtHospitalDistrict").val(data[0].district);
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
        if (!source.id.includes("common") && !source.id.includes("weekend")) {
            return;
        }

        if ($(source).attr("size") == '1' && $(source).find("option").length > 5) {
            event.preventDefault();
        }
    });
    //on click event
    $("select").on('click', function (event) {
        var source = event.currentTarget;
        if (!source.id.includes("common") && !source.id.includes("weekend")) {
            return;
        }

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
        if (!source.id.includes("common") && !source.id.includes("weekend")) {
            return;
        }

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
        if (!source.id.includes("common") && !source.id.includes("weekend")) {
            return;
        }

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
        $('#chkAddCurrentAvailability').each(function () { this.checked = !this.checked; });
    });

    $('#btnRemoveCurrentAvailability').on('click', function () {
        $('#chkAddCurrentAvailability').each(function () { this.checked = !this.checked; });
    });

    function SetDegree() {
        var result = "";
        $.each(Degree, function (index, value) {
            if (value != "") {
                if (index == 0) {
                    result = value;
                }
                else {
                    result = result + ", " + value;
                }
            }
            else {
                return false;
            }
        });
        $("#txtDegree").val(result);
        //$("#txtDegree").val(value);

        //$("#txtDegree").attr("value", result).trigger('keyup');
    }

    $(".divDegree").on('change', "select.education", function (event) {
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
        if (count == 1) {
            $("#divDegree1").toggle();
        }
        else if (count < 5) {
            let item = $("#divDegree1").html().replaceAll("Degree1", "Degree" + count);
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