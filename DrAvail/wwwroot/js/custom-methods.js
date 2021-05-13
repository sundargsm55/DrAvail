$(document).ready(function () {
    var MHours = ["00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14"];
    var Times = ["00", "15", "30", "45"];
    var EHours = ["14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "00"];

    //setting value to availabilityType
    $("#availabilityType").val("Common");
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

    $("#dob").on('change',function (event) {
        var dob = event.currentTarget.value.split("/")[2];
        $("#age").val(year - parseInt(dob));
    });

    function CheckRegistrationNumberExists(registrationNumber) {
        if (registrationNumber != null && registrationNumber.length > 5) {
            var url = window.location.origin + "/Doctors/DoctorExistsByRegistrationNumber";
            $.getJSON(url, { registrationNumber: registrationNumber }, function (data) {
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
    $("#txtRegistrationNumber").on('change',function (event) {
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
        }
    }

    function SetTimeFromHourMinute(target) {
        if (target != null && target.endsWith("Time")) {
            var Hour = $(target.replace("Time", "Hour")).find(":selected").val();
            var Minute = $(target.replace("Time", "Minute")).find(":selected").val();
            $(target).val(Hour + ":" + Minute);
            //console.log("commonMorningStartTime: " + $(target).val());

        }
    }

    function setCommonMorningStartTime() {
        var commonMorningStartHour = $("#commonMorningStartHour").find(":selected").val();
        var commonMorningStartMinute = $("#commonMorningStartMinute").find(":selected").val();
        //var commonMorningStartTime = new Date(year, month, day, commonMorningStartHour, commonMorningStartMinute);
        $("#commonMorningStartTime").val(commonMorningStartHour + ":" + commonMorningStartMinute);
        //console.log("commonMorningStartTime: " + $("#commonMorningStartTime").val());
    }

    function setCommonMorningEndTime() {
        var commonMorningEndHour = $("#commonMorningEndHour").find(":selected").val();
        var commonMorningEndMinute = $("#commonMorningEndMinute").find(":selected").val();
        //var commonMorningEndTime = new Date(year, month, day, commonMorningEndHour, commonMorningEndMinute);
        $("#commonMorningEndTime").val(commonMorningEndHour + ":" + commonMorningEndMinute);
        //console.log("commonMorningEndTime: " + $("#commonMorningEndTime").val());
    }

    function setCommonEveningStartTime() {
        var commonEveningStartHour = $("#commonEveningStartHour").find(":selected").val();
        var commonEveningStartMinute = $("#commonEveningStartMinute").find(":selected").val();
        //var commonEveningStartTime = new Date(year, month, day, commonEveningStartHour, commonEveningStartMinute);
        $("#commonEveningStartTime").val(commonEveningStartHour + ":" + commonEveningStartMinute);
        //console.log("commonEveningStartTime: " + $("#commonEveningStartTime").val());
    }

    function setCommonEveningEndTime() {
        var commonEveningEndHour = $("#commonEveningEndHour").find(":selected").val();
        var commonEveningEndMinute = $("#commonEveningEndMinute").find(":selected").val();
        //var commonEveningEndTime = new Date(year, month, day, commonEveningEndHour, commonEveningEndMinute);
        $("#commonEveningEndTime").val(commonEveningEndHour + ":" + commonEveningEndMinute);
        //console.log("commonEveningEndTime: " + $("#commonEveningEndTime").val());
    }

    function setWeekendMorningStartTime() {
        var weekendMorningStartHour = $("#weekendMorningStartHour").find(":selected").val();
        var weekendMorningStartMinute = $("#weekendMorningStartMinute").find(":selected").val();
        //var weekendMorningStartTime = new Date(year, month, day, weekendMorningStartHour, weekendMorningStartMinute);
        $("#weekendMorningStartTime").val(weekendMorningStartHour + ":" + weekendMorningStartMinute);
        //console.log("weekendMorningStartTime: " + $("#weekendMorningStartTime").val());
    }

    function setWeekendMorningEndTime() {
        var weekendMorningEndHour = $("#weekendMorningEndHour").find(":selected").val();
        var weekendMorningEndMinute = $("#weekendMorningEndMinute").find(":selected").val();
        //var weekendMorningEndTime = new Date(year, month, day, weekendMorningEndHour, weekendMorningEndMinute);
        $("#weekendMorningEndTime").val(weekendMorningEndHour + ":" + weekendMorningEndMinute);
        //console.log("weekendMorningEndTime: " + $("#weekendMorningEndTime").val());
    }

    function setWeekendEveningStartTime() {
        var weekendEveningStartHour = $("#weekendEveningStartHour").find(":selected").val();
        var weekendEveningStartMinute = $("#weekendEveningStartMinute").find(":selected").val();
        //var weekendEveningStartTime = new Date(year, month, day, weekendEveningStartHour, weekendEveningStartMinute);
        $("#weekendEveningStartTime").val(weekendEveningStartHour + ":" + weekendEveningStartMinute);
        //console.log("weekendEveningStartTime: " + $("#weekendEveningStartTime").val());
    }

    function setWeekendEveningEndTime() {
        var weekendEveningEndHour = $("#weekendEveningEndHour").find(":selected").val();
        var weekendEveningEndMinute = $("#weekendEveningEndMinute").find(":selected").val();
        //var weekendEveningEndTime = new Date(year, month, day, weekendEveningEndHour, weekendEveningEndMinute);
        $("#weekendEveningEndTime").val(weekendEveningEndHour + ":" + weekendEveningEndMinute);
        //console.log("weekendEveningEndTime: " + $("#weekendEveningEndTime").val());
    }

    function setDefaultWeekendTiming(timing = "00:00") {
        //Morning start time
        $("#weekendMorningStartTime").val(timing);
        //console.log("weekendMorningStartTime: " + $("#weekendMorningStartTime").val());
        //morning end time
        $("#weekendMorningEndTime").val(timing);
        //console.log("weekendMorningEndTime: " + $("#weekendMorningEndTime").val());
        //evening start time
        $("#weekendEveningStartTime").val(timing);
        //console.log("weekendEveningStartTime: " + $("#weekendEveningStartTime").val());
        //evening end time
        $("#weekendEveningEndTime").val(timing);
        //console.log("weekendEveningEndTime: " + $("#weekendEveningEndTime").val());
    }

    $('#btnSave').on('click',function (event) {
        //var status = CheckRegistrationNumberExists($("#txtRegistrationNumber").val());
        //console.log("CheckRegistrationNumberExists: " + status);
        if (!CheckRegistrationNumberExists($("#txtRegistrationNumber").val())) {
            //for common days
            //setCommonMorningStartTime();
            console.log("Inside if part CheckRegistrationNumberExists");
            SetTimeFromHourMinute("#commonMorningStartTime");
            SetTimeFromHourMinute("#commonMorningEndTime");
            SetTimeFromHourMinute("#commonEveningStartTime");
            SetTimeFromHourMinute("#commonEveningEndTime");

            /*setCommonMorningEndTime();
            setCommonEveningStartTime();
            setCommonEveningEndTime();*/
            //for weekends
            var availableOnWeekend = $('#chkAvailableOnWeekend:checked').length
            //console.log("availableOnWeekend: " + availableOnWeekend);
            if (availableOnWeekend != 0) {

                SetTimeFromHourMinute("#weekendMorningStartTime");
                SetTimeFromHourMinute("#weekendMorningEndTime");
                SetTimeFromHourMinute("#weekendEveningStartTime");
                SetTimeFromHourMinute("#weekendEveningEndTime");
/*
                setWeekendMorningStartTime();
                setWeekendMorningEndTime();
                setWeekendEveningStartTime();
                setWeekendEveningEndTime();*/
            }
            else {
                setDefaultWeekendTiming();
            }
            $('#myForm').submit(); //submit the form
        }
        else {
            //now it will submit regardless
            //but need to prevent from submit if above check fails
            console.log("else CheckRegistrationNumberExists");

            event.preventDefault();
            return;
        }


    });

    //commonMorningStartHour
    $("#commonMorningStartHour").change(function () {
        //debugger;
        var startHour = $("#commonMorningStartHour").find(":selected").val();
        var startminute = $("#commonMorningStartMinute").find(":selected").val();
        if (jQuery.inArray(startHour, MHours) != -1) {
            var Hindex = MHours.indexOf(startHour);
            //console.log("Index: " + index);
            var items = "";
            //console.log("Timing array length: " + MHours.length);
            $.each(MHours, function (i, value) {
                //console.log("i: " + i)
                if (i >= Hindex) {
                    items += "<option value='" + value + "'>" + value + "</option>";
                }
            });
            var Tindex = Times.indexOf(startminute);
            var timings = "";
            $.each(Times, function (i, value) {
                if (i > Tindex) {
                    timings += "<option value='" + value + "'>" + value + "</option>";
                }
            })
            $("#commonMorningEndHour").html(items);
            $("#commonMorningEndMinute").html(timings);
        }
    });

    //commonMorningStartMinute
    $("#commonMorningStartMinute").change(function () {
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
    $("#commonMorningEndHour").change(function () {
        var startHour = $("#commonMorningStartHour").find(":selected").val();
        var endHour = $("#commonMorningEndHour").find(":selected").val();
        var startHourIndex = MHours.indexOf(startHour);
        var endHourIndex = MHours.indexOf(endHour);
        var timings = "";

        if (endHourIndex == (MHours.length - 1)) {
            timings += "<option value='" + Times[0] + "'>" + Times[0] + "</option>";
            //console.log("timings::: " + timings);
            $("#commonMorningEndMinute").html(timings);
        }
        else if (endHourIndex > startHourIndex) {
            //var timings = "";
            $.each(Times, function (i, value) {
                timings += "<option value='" + value + "'>" + value + "</option>";
            });
            $("#commonMorningEndMinute").html(timings);
        }
        else {
            var startminute = $("#commonMorningStartMinute").find(":selected").val();
            var Tindex = Times.indexOf(startminute);
            $.each(Times, function (i, value) {
                if (i > Tindex) {
                    timings += "<option value='" + value + "'>" + value + "</option>";
                }
            });
            $("#commonMorningEndMinute").html(timings);
        }
    });

    //commonEveningStartHour
    $("#commonEveningStartHour").change(function () {
        //debugger;
        var startHour = $("#commonEveningStartHour").find(":selected").val();
        var startminute = $("#commonEveningStartMinute").find(":selected").val();
        if (jQuery.inArray(startHour, EHours) != -1) {
            var Hindex = EHours.indexOf(startHour);
            //console.log("Index: " + index);
            var items = "";
            //console.log("Timing array length: " + Hours.length);
            $.each(EHours, function (i, value) {
                //console.log("i: " + i)
                if (i >= Hindex) {
                    items += "<option value='" + value + "'>" + value + "</option>";
                }
            });
            var Tindex = Times.indexOf(startminute);
            var timings = "";
            $.each(Times, function (i, value) {
                if (i > Tindex) {
                    timings += "<option value='" + value + "'>" + value + "</option>";
                }
            })
            $("#commonEveningEndHour").html(items);
            $("#commonEveningEndMinute").html(timings);
        }
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
            if (jQuery.inArray(startHour, MHours) != -1) {
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
    $("#commonEveningEndHour").change(function () {
        var startHour = $("#commonEveningStartHour").find(":selected").val();
        var endHour = $("#commonEveningEndHour").find(":selected").val();
        var endHourIndex = EHours.indexOf(endHour);
        var startHourindex = EHours.indexOf(startHour);
        var timings = "";
        var items = "";

        if (endHourIndex == EHours.length - 1) {
            timings += "<option value='" + Times[0] + "'>" + Times[0] + "</option>";
            //items += "<option value='" + EHours[EHours.length - 1] + "'>" + EHours[EHours.length - 1] + "</option>";
            //$("#commonEveningEndHour").html(items);
            $("#commonEveningEndMinute").html(timings);
        }
        else if (endHourIndex > startHourindex) {
            $.each(Times, function (i, value) {
                timings += "<option value='" + value + "'>" + value + "</option>";
            });
            $("#commonEveningEndMinute").html(timings);
        }
        else {
            var startminute = $("#commonEveningStartMinute").find(":selected").val();
            var Tindex = Times.indexOf(startminute);
            var timings = "";
            $.each(Times, function (i, value) {
                if (i > Tindex) {
                    timings += "<option value='" + value + "'>" + value + "</option>";
                }
            });
            $("#commonEveningEndMinute").html(timings);
        }
    });

    //weekends
    //weekendMorningStartHour
    $("#weekendMorningStartHour").change(function () {
        //debugger;
        var startHour = $("#weekendMorningStartHour").find(":selected").val();
        var startminute = $("#weekendMorningStartMinute").find(":selected").val();
        if (jQuery.inArray(startHour, MHours) != -1) {
            var Hindex = MHours.indexOf(startHour);
            //console.log("Index: " + index);
            var items = "";
            //console.log("Timing array length: " + MHours.length);
            $.each(MHours, function (i, value) {
                //console.log("i: " + i)
                if (i >= Hindex) {
                    items += "<option value='" + value + "'>" + value + "</option>";
                }
            });
            var Tindex = Times.indexOf(startminute);
            var timings = "";
            $.each(Times, function (i, value) {
                if (i > Tindex) {
                    timings += "<option value='" + value + "'>" + value + "</option>";
                }
            })
            $("#weekendMorningEndHour").html(items);
            $("#weekendMorningEndMinute").html(timings);
        }
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
    $("#weekendMorningEndHour").change(function () {
        var startHour = $("#weekendMorningStartHour").find(":selected").val();
        var endHour = $("#weekendMorningEndHour").find(":selected").val();
        var startHourIndex = MHours.indexOf(startHour);
        var endHourIndex = MHours.indexOf(endHour);
        var timings = "";

        if (endHourIndex == MHours.length - 1) {
            //console.log("endHourIndex == MHours.length - 1::: true");
            $("#weekendMorningEndMinute").empty();
            timings += "<option value='" + Times[0] + "'>" + Times[0] + "</option>";
            $("#weekendMorningEndMinute").html(timings);
        }
        else if (endHourIndex > startHourIndex) {
            // console.log("endHourIndex > startHourIndex::: true");

            $.each(Times, function (i, value) {
                timings += "<option value='" + value + "'>" + value + "</option>";
            });
            $("#weekendMorningEndMinute").html(timings);
        }
        else {
            var startminute = $("#weekendMorningStartMinute").find(":selected").val();
            var Tindex = Times.indexOf(startminute);
            $.each(Times, function (i, value) {
                if (i > Tindex) {
                    timings += "<option value='" + value + "'>" + value + "</option>";
                }
            });
            $("#weekendMorningEndMinute").html(timings);
        }
    });

    //weekendEveningStartHour
    $("#weekendEveningStartHour").change(function () {
        //debugger;
        var startHour = $("#weekendEveningStartHour").find(":selected").val();
        var startminute = $("#weekendEveningStartMinute").find(":selected").val();
        if (jQuery.inArray(startHour, EHours) != -1) {
            var Hindex = EHours.indexOf(startHour);
            //console.log("Index: " + index);
            var items = "";
            //console.log("Timing array length: " + Hours.length);
            $.each(EHours, function (i, value) {
                //console.log("i: " + i)
                if (i >= Hindex) {
                    items += "<option value='" + value + "'>" + value + "</option>";
                }
            });
            var Tindex = Times.indexOf(startminute);
            var timings = "";
            $.each(Times, function (i, value) {
                if (i > Tindex) {
                    timings += "<option value='" + value + "'>" + value + "</option>";
                }
            })
            $("#weekendEveningEndHour").html(items);
            $("#weekendEveningEndMinute").html(timings);
        }
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
    $("#weekendEveningEndHour").change(function () {
        var startHour = $("#weekendEveningStartHour").find(":selected").val();
        var endHour = $("#weekendEveningEndHour").find(":selected").val();
        var endHourIndex = EHours.indexOf(endHour);
        var startHourIndex = EHours.indexOf(startHour);
        var timings = "";
        var items = "";

        if (endHourIndex == EHours.length - 1) {
            timings += "<option value='" + Times[0] + "'>" + Times[0] + "</option>";
            $("#weekendEveningEndMinute").html(timings);
        }
        else if (endHourIndex > startHourIndex) {
            //var timings = "";
            $.each(Times, function (i, value) {
                timings += "<option value='" + value + "'>" + value + "</option>";
            });
            $("#weekendEveningEndMinute").html(timings);
        }
        else {
            var startminute = $("#weekendEveningStartMinute").find(":selected").val();
            var Tindex = Times.indexOf(startminute);
            //var timings = "";
            $.each(Times, function (i, value) {
                if (i > Tindex) {
                    timings += "<option value='" + value + "'>" + value + "</option>";
                }
            });
            $("#weekendEveningEndMinute").html(timings);
        }
    });

    function CheckWeekendSameAsCommonDays() {
        if ($("#sameAsCommonDays:checked").length != 0) {
            return true;
        }
        return false;
    }

    function CopyToWeekend(source) {
        //console.log("CopyToWeekend source: " + source);
        var target = source.replace("common", "weekend");
        //console.log("source: " + source);
        //console.log("destination: " + destination);

        /*$(destination).find("option").filter(function () {
            return $(source).find(":selected").val() === $(this).text();
        }).attr("selected", true).trigger('change');*/

        $(target).val($(source).find(":selected").val()).trigger('change');

        //console.log("source value: " + $(source).find(":selected").val());
        //console.log("destination value: " + $(destination).find(":selected").val());
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

    $("#txtExperience").on('change', function () {
        //debugger;
        var exp = $("#txtExperience").val();
        //console.log("\nexp: " + exp);

        var dob = $("#dob").val();
        //console.log("\ndob: " + dob);
        if (dob !== "") {
            var age = new Date().getFullYear() - new Date(dob).getFullYear();
            //console.log("Today date: " + new Date().toString());
            //console.log("\nDOB: " + new Date(dob).toString());
            console.log("\nAge: " + age);
            if (age > 24) {
                var diff = age - 25;
                //console.log("\ndiff: " + diff);
                if (exp >= 0 && exp <= diff) {
                    //alert('ok');
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
        if (source.id == "Speciality") return;
        //console.log("Option lenght: " + $(source).find("option").length);
        if ($(source).attr("size") == '1' && $(source).find("option").length > 5) {
            event.preventDefault();
        }
    });
    //on click event
    $("select").on('click', function (event) {
        var source = event.currentTarget;
        if (source.id == "Speciality") return;
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
        if (source.id == "Speciality") return;
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
        if (source.id == "Speciality") return;


        $(source).removeClass("selectOpen");
        $(source).attr("size", '1');
    });

    $('#selHospitalId').one('change click',function () {
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


});