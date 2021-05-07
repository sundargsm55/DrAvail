$(document).ready(function () {
    var MHours = ["00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14"];
    var Times = ["00", "15", "30", "45"];
    var EHours = ["14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "00"];

    //setting value to availabilityType
    $("#availabilityType").val("Common");

    var today = new Date();
    var year = today.getFullYear();
    var month = today.getMonth();
    var day = today.getDay();
    $('#dob').datepicker({
        minDate: new Date(year - 125, month, day),
        maxDate: new Date(year - 25, month, day),
        setDate: new Date(year - 25, month, day)
    });

    $("#dob").change(function (event) {
        var dob = event.currentTarget.value.split("/")[2];
        $("#age").val(year - parseInt(dob));
    });

    $(window).on('load', function () {
        if ($("#txtPincode").val().length == 6) {
            $("#txtPincode").trigger('keyup');
        }
        var id = $("#selHospitalId").find(":selected").val();
        if (id) {
            $("#selHospitalId").trigger('change');
        }
    })

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
        console.log("weekendMorningStartTime: " + $("#weekendMorningStartTime").val());
    }

    function setWeekendMorningEndTime() {
        var weekendMorningEndHour = $("#weekendMorningEndHour").find(":selected").val();
        var weekendMorningEndMinute = $("#weekendMorningEndMinute").find(":selected").val();
        //var weekendMorningEndTime = new Date(year, month, day, weekendMorningEndHour, weekendMorningEndMinute);
        $("#weekendMorningEndTime").val(weekendMorningEndHour + ":" + weekendMorningEndMinute);
        console.log("weekendMorningEndTime: " + $("#weekendMorningEndTime").val());
    }

    function setWeekendEveningStartTime() {
        var weekendEveningStartHour = $("#weekendEveningStartHour").find(":selected").val();
        var weekendEveningStartMinute = $("#weekendEveningStartMinute").find(":selected").val();
        //var weekendEveningStartTime = new Date(year, month, day, weekendEveningStartHour, weekendEveningStartMinute);
        $("#weekendEveningStartTime").val(weekendEveningStartHour + ":" + weekendEveningStartMinute);
        console.log("weekendEveningStartTime: " + $("#weekendEveningStartTime").val());
    }

    function setWeekendEveningEndTime() {
        var weekendEveningEndHour = $("#weekendEveningEndHour").find(":selected").val();
        var weekendEveningEndMinute = $("#weekendEveningEndMinute").find(":selected").val();
        //var weekendEveningEndTime = new Date(year, month, day, weekendEveningEndHour, weekendEveningEndMinute);
        $("#weekendEveningEndTime").val(weekendEveningEndHour + ":" + weekendEveningEndMinute);
        console.log("weekendEveningEndTime: " + $("#weekendEveningEndTime").val());
    }

    function setDefaultWeekendTiming(timing = "00:00") {
        //Morning start time
        $("#weekendMorningStartTime").val(timing);
        console.log("weekendMorningStartTime: " + $("#weekendMorningStartTime").val());
        //morning end time
        $("#weekendMorningEndTime").val(timing);
        console.log("weekendMorningEndTime: " + $("#weekendMorningEndTime").val());
        //evening start time
        $("#weekendEveningStartTime").val(timing);
        console.log("weekendEveningStartTime: " + $("#weekendEveningStartTime").val());
        //evening end time
        $("#weekendEveningEndTime").val(timing);
        console.log("weekendEveningEndTime: " + $("#weekendEveningEndTime").val());
    }

    $('#btnCreate').click(function () {
        //for common days
        setCommonMorningStartTime();
        setCommonMorningEndTime();
        setCommonEveningStartTime();
        setCommonEveningEndTime();
        //for weekends
        var availableOnWeekend = $('#chkAvailableOnWeekend:checked').length
        console.log("availableOnWeekend: " + availableOnWeekend);
        if (availableOnWeekend) {
            setWeekendMorningStartTime();
            setWeekendMorningEndTime();
            setWeekendEveningStartTime();
            setWeekendEveningEndTime();
        }
        else {
            setDefaultWeekendTiming();
        }

        //$('#btnCreate').submit();

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

        if (endHourIndex == MHours.length - 1) {
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
        console.log("Weekedn Morning Start Hour Value: " + startHour);
        console.log("Weekedn Morning Start Hour Index: " + startHourIndex);

        console.log("Weekedn Morning End Hour Value: " + endHour);
        console.log("Weekedn Morning End Hour Index: " + endHourIndex);

        if (endHourIndex == MHours.length - 1) {
            timings += "<option value='" + Times[0] + "'>" + Times[0] + "</option>";
            //console.log("timings::: " + timings);
            $("#commonMorningEndMinute").html(timings);
        }
        else if (endHourIndex > startHourIndex) {
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
            //items += "<option value='" + EHours[EHours.length - 1] + "'>" + EHours[EHours.length - 1] + "</option>";
            //$("#commonEveningEndHour").html(items);
            $("#commonEveningEndMinute").html(timings);
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

    $("#sameAsCommonDays").click(function () {
        var n = $("#sameAsCommonDays:checked").length;
        //console.log("n::: " + n);
        if (n != 0) {
            //var timings = "";
            //$.each(Times, function (i, value) {
            //    timings += "<option value='" + value + "'>" + value + "</option>";
            //});

            var morningStartHour = $("#commonMorningStartHour").find(":selected").val();
            var morningEndHour = $("#commonMorningEndHour").find(":selected").val();
            var morningStartMinute = $("#commonMorningStartMinute").find(":selected").val();
            var morningEndMinute = $("#commonMorningEndMinute").find(":selected").val();

            var eveningStartHour = $("#commonEveningStartHour").find(":selected").val();
            var eveningEndHour = $("#commonEveningEndHour").find(":selected").val();
            var eveningStartMinute = $("#commonEveningStartMinute").find(":selected").val();
            var eveningEndMinute = $("#commonEveningEndMinute").find(":selected").val();

            //$("#weekendMorningStartHour").find("option:contains(" + morningStartHour + ")").attr("selected", true).trigger('change');
            $("#weekendMorningStartHour").find("option").filter(function () {
                return morningStartHour === $(this).text();
            }).attr("selected", true).trigger('change');

            $("#weekendMorningStartMinute").find("option:contains(" + morningStartMinute + ")").attr("selected", true).trigger('change');

            //$("#weekendMorningEndHour").find("option:contains(" + morningEndHour + ")").attr("selected", true).trigger('change');
            $("#weekendMorningEndHour").find("option").filter(function () {
                return morningEndHour === $(this).text();
            }).attr("selected", true).trigger('change');

            $("#weekendMorningEndMinute").find("option:contains(" + morningEndMinute + ")").attr("selected", true);

            $("#weekendEveningStartHour").find("option:contains(" + eveningStartHour + ")").attr("selected", true).trigger('change');
            $("#weekendEveningStartMinute").find("option:contains(" + eveningStartMinute + ")").attr("selected", true).trigger('change');
            $("#weekendEveningEndHour").find("option:contains(" + eveningEndHour + ")").attr("selected", true).trigger('change');
            $("#weekendEveningEndMinute").find("option:contains(" + eveningEndMinute + ")").attr("selected", true);
            toggleWeekendTiming(true);
        }
        else {
            toggleWeekendTiming(false);
        }

    });

    function toggleWeekendTiming(status) {

        $("#weekendMorningStartHour").attr("disabled", status);
        $("#weekendMorningEndHour").attr("disabled", status);
        $("#weekendMorningStartMinute").attr("disabled", status);
        $("#weekendMorningEndMinute").attr("disabled", status);

        $("#weekendEveningStartHour").attr("disabled", status);
        $("#weekendEveningEndHour").attr("disabled", status);
        $("#weekendEveningStartMinute").attr("disabled", status);
        $("#weekendEveningEndMinute").attr("disabled", status);
    }

    $("#txtExperience").change(function () {
        //debugger;
        var exp = $("#txtExperience").val();
        console.log("\nexp: " + exp);

        var dob = $("#dob").val();
        console.log("\ndob: " + dob);
        if (dob != "") {
            var age = new Date().getFullYear() - new Date(dob).getFullYear();
            console.log("Today date: " + new Date().toString());
            console.log("\nDOB: " + new Date(dob).toString());
            console.log("\nAge: " + age);
            if (age > 24) {
                var diff = age - 25;
                console.log("\ndiff: " + diff);
                if (exp >= 0 && exp <= diff) {
                    alert('ok');
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
    $("#txtPincode").keyup(function () {
        //debugger;
        //var source = "#txtPincode";
        //console.log("Pincode: " + $("#txtPincode").val());
        if ($("#txtPincode").val().length == 6) {
            var url = "Doctors/GetLocations";
            //console.log("Url: " + url);
            $.getJSON(url, { Pincode: $("#txtPincode").val() }, function (data) {
                var items = "";
                if (data.length == 0) {
                    console.log("No location found!!!");
                    return false;
                }
                $("#City").empty();
                $("#txtDistrict").val(data[0].dis);
                //$("txtDistrict").text = data[0].dis;
                //console.log(data[0].dis);
                $.each(data, function (i, location) {
                    items += "<option value='" + location.locality + "'>" + location.locality + "</option>";
                });
                $("#City").html(items);
            });
        }
    });
    //for hostpital
    $("#txtHospitalPincode").keyup(function () {
        //debugger;
        //var source = "#txtPincode";
        //console.log("Pincode: " + $("#txtPincode").val());
        if ($("#txtHospitalPincode").val().length == 6) {
            var url = "Doctors/GetLocations";
            //console.log("Url: " + url);
            $.getJSON(url, { Pincode: $("#txtHospitalPincode").val() }, function (data) {
                var items = "";
                if (data.length == 0) {
                    console.log("No location found!!!");
                    return false;
                }
                $("#HospitalCity").empty();
                $("#txtHospitalDistrict").val(data[0].dis);
                //$("txtDistrict").text = data[0].dis;
                //console.log(data[0].dis);
                $.each(data, function (i, location) {
                    items += "<option value='" + location.locality + "'>" + location.locality + "</option>";
                });
                $("#HospitalCity").html(items);
            });
        }
    });

    //City select list
    //on mousedown event
    $("select").mousedown(function (event) {
        var source = event.currentTarget;
        if (source.id == "Speciality") return;
        //console.log("Option lenght: " + $(source).find("option").length);
        if ($(source).attr("size") == '1' && $(source).find("option").length > 5) {
            event.preventDefault();
        }
    });
    //on click event
    $("select").click(function (event) {
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
            $(source).removeClass("selectOpen");
            $(source).attr("size", '1');
        }
    });

    //restting when loses focus
    $("select").focusout(function (event) {
        var source = event.currentTarget;
        if (source.id == "Speciality") return;

        $(source).removeClass("selectOpen");
        $(source).attr("size", '1');
    });

    $("select").mouseleave(function (event) {
        var source = event.currentTarget;
        if (source.id == "Speciality") return;

        $(source).removeClass("selectOpen");
        $(source).attr("size", '1');
    });

    $('#selHospitalId').change(function () {
        var url = "Hospitals/FetchHospitalDetails";
        $.getJSON(url, { id: $("#selHospitalId").find(":selected").val() }, function (data) {
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
                    $("#txtHospitalEmail").val(data.email);
                    $("#txtHospitalPhone").val(data.phone);
                }
            }
        });
    });
});