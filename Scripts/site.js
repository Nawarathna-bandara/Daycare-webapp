  
    $(function() {
        
        $('#mainContent_btSave').prop('disabled', false);

        $("#mainContent_txtFatherIFSUser").prop("disabled", !this.checked);
        $("#mainContent_txtMotherIFSUser").prop("disabled", !this.checked);
        $("#mainContent_txtFatherEmail").prop("disabled", !this.checked);
        $("#mainContent_txtMotherEmail").prop("disabled", !this.checked);

        $("#mainContent_chkIsFatherIFS").click(function() {
            $("#mainContent_txtFatherIFSUser").prop("disabled", !this.checked);
            $("#mainContent_txtFatherEmail").prop("disabled", !this.checked);
        });

        $("#mainContent_chkIsMotherIFS").click(function () {
            $("#mainContent_txtMotherIFSUser").prop("disabled", !this.checked);
            $("#mainContent_txtMotherEmail").prop("disabled", !this.checked);
        });

        if( $("#mainContent_rdoNewParent").is(":checked") )
            $('div[id$=IFSUserName]').hide();
        else
            $('div[id$=IFSUserName]').show();

        $("#mainContent_rdoNewParent").click(function () {

            $('div[id$=IFSUserName]').hide();

            $('#mainContent_txtFatherName').val('');
            $('#mainContent_txtFatherContact').val('');
            $('#mainContent_txtMotherName').val('');
            $('#mainContent_txtMotherContact').val('');
            $('#mainContent_txtContactNumber').val('');
            $('#mainContent_txtAddress').val('');
            $('#mainContent_txtMotherEmail').val('');
            $('#mainContent_txtFatherEmail').val('');
            $('#mainContent_txtMotherIFSUser').val('');
            $('#mainContent_txtFatherIFSUser').val('');
            $('#mainContent_txtNote').val('');
            $('#mainContent_chkIsFatherIFS').prop('checked', false);
            $('#mainContent_chkIsMotherIFS').prop('checked', false);
            $("#mainContent_txtFatherIFSUser").prop("disabled", true);
            $("#mainContent_txtFatherEmail").prop("disabled", true);
            $("#mainContent_txtMotherIFSUser").prop("disabled", true);
            $("#mainContent_txtMotherEmail").prop("disabled", true);

            $('#mainContent_txtFatherName').prop('readonly', false);
            $('#mainContent_chkIsFatherIFS').prop('disabled', false);
            $('#mainContent_txtFatherContact').prop('readonly', false);
            $('#mainContent_txtMotherName').prop('readonly', false);
            $('#mainContent_chkIsMotherIFS').prop('disabled', false);
            $('#mainContent_txtMotherContact').prop('readonly', false);



        });
        $("#mainContent_rdoRegParent").click(function () {

            $('div[id$=IFSUserName]').show();

            $('#mainContent_txtFatherName').val('');
            $('#mainContent_txtFatherContact').val('');
            $('#mainContent_txtMotherName').val('');
            $('#mainContent_txtMotherContact').val('');
            $('#mainContent_txtContactNumber').val('');
            $('#mainContent_txtAddress').val('');
            $('#mainContent_txtMotherEmail').val('');
            $('#mainContent_txtFatherEmail').val('');
            $('#mainContent_txtMotherIFSUser').val('');
            $('#mainContent_txtFatherIFSUser').val('');
            $('#mainContent_txtNote').val('');
            $('#mainContent_chkIsFatherIFS').prop('checked', false);
            $('#mainContent_chkIsMotherIFS').prop('checked', false);
            $("#mainContent_txtFatherIFSUser").prop("disabled", true);
            $("#mainContent_txtFatherEmail").prop("disabled", true);
            $("#mainContent_txtMotherIFSUser").prop("disabled", true);
            $("#mainContent_txtMotherEmail").prop("disabled", true);

            $('#mainContent_txtFatherName').prop('readonly', true);
            $('#mainContent_chkIsFatherIFS').prop('disabled', true);
            $('#mainContent_txtFatherContact').prop('readonly', true);
            $('#mainContent_txtMotherName').prop('readonly', true);
            $('#mainContent_chkIsMotherIFS').prop('disabled', true);
            $('#mainContent_txtMotherContact').prop('readonly', true);


            $.ajax({
                type: "POST",
                url: "AddKids.aspx/GetParentUseNames",
                dataType: "json",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: true,
                success: function (data) {
                    var datafromServer = data.d.split(String.fromCharCode(31));
                    $("[id$='mainContent_txtIFSUserName']").autocomplete({
                        source: datafromServer
                    });
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
            
        });
    });

$(document).ready(function () {

    $("#mainContent_txtKidDob").datepicker({
        dateFormat: "yy-mm-dd", changeMonth: true, changeYear: true, maxDate:"+0d", minDate: "-10Y"

    }).attr('readonly', 'true');

});

$(document).ready(function () {

    $.validator.messages.required = '<font color=red> &nbsp; * </font>';

    jQuery.validator.addMethod("isValidMoM", function (value, element) {

        var val = false;

        if ($('#mainContent_chkIsMotherIFS').is(':checked')) {
                
            val = ( ($('#mainContent_txtMotherIFSUser').val().length > 0) &&  ($('#mainContent_txtMotherEmail').val().length > 0) )
        }
        return val;

    }, "<font color='red'>&nbsp; * </font>");

    jQuery.validator.addMethod("isValidFather", function (value, element) {

        var val = false;

        if ($('#mainContent_chkIsFatherIFS').is(':checked')) {

            val = ( ($('#mainContent_txtFatherIFSUser').val().length > 0) && ($('#mainContent_txtFatherEmail').val().length > 0) )
        }

        return val;

    }, "<font color='red'>&nbsp; * </font>");

    $("#mainFrom").validate({

        rules: {
            'ctl00$mainContent$txtKidDob': { required: true, date: true },
            'ctl00$mainContent$txtMotherIFSUser': { required: true, isValidMoM: true },
            'ctl00$mainContent$txtFatherIFSUser': { required: true, isValidFather: true },
            'ctl00$mainContent$txtFatherEmail': { required: true, email: true },
            'ctl00$mainContent$txtMotherEmail': { required: true, email: true },
            'ctl00$mainContent$DropDownGender': { required: true },
            'ctl00$mainContent$DropDownYear': { required: true },
            'ctl00$mainContent$txtFatherContact': { required: true},
            'ctl00$mainContent$txtMotherContact': { required: true}
        },
        messages: {
            'ctl00$mainContent$DropDownGender': '<font color=red size=1em> &nbsp; Please Select </font>',
            'ctl00$mainContent$DropDownYear': '<font color=red size=1em> &nbsp; Please Select </font>',
            'ctl00$mainContent$txtFatherEmail': '<font color=red size=1em> &nbsp;</br> Invalid Email Format. Please enter valid IFS email</font>',
            'ctl00$mainContent$txtMotherEmail': '<font color=red size=1em> &nbsp;</br> Invalid Email Format. Please enter valid IFS email</font>',
            'ctl00$mainContent$txtFatherContact': '<font color=red size=1em> &nbsp;</br> Invalid Format. Please enter valid phone number </font>',
            'ctl00$mainContent$txtMotherContact': '<font color=red size=1em> &nbsp;</br> Invalid Format. Please enter valid phone number </font>'
        }

    });
});

var globle = true;
   
$(document).ready(function () {

    $('#mainContent_btSave').click(function () {
            
        //if ($('#mainContent_btSave').clicked()) {
            alert(($('#mainFrom').valid()));
        //}

        if (($('#mainFrom').valid())){

            if ($('#mainContent_DropDownGender :selected').val() == "") {
                alert('Please Select Gender');
                return false;
            }

            else if ($('#mainContent_DropDownYear :selected').val() == ""){
                alert('Please Select Subscription Year');
                return false;
            }

            //if(!$('#mainContent_btUserLoad').clicked()){

            if (!$("#mainContent_chkIsFatherIFS").is(":checked") && !$("#mainContent_chkIsMotherIFS").is(":checked")&&globle) {

                alert('Parent IFS username is required!');
                return false;
            }

            //}

           // $('#mainContent_btSave').prop('disabled', true);
            alert('Hiii');
             return true;
        }               
        else
            return false;
    });

    $('#mainContent_btUserLoad').click(function () {
           

        $('#mainContent_txtFatherName').val('');
        $('#mainContent_txtFatherContact').val('');
        $('#mainContent_txtMotherName').val('');
        $('#mainContent_txtMotherContact').val('');
        $('#mainContent_txtContactNumber').val('');
        $('#mainContent_txtAddress').val('');
        $('#mainContent_txtMotherEmail').val('');
        $('#mainContent_txtFatherEmail').val('');
        $('#mainContent_txtMotherIFSUser').val('');
        $('#mainContent_txtFatherIFSUser').val('');


        var userName_ = $("#mainContent_txtIFSUserName").val();

        globle = false;

        if (userName_ == "") {
            alert("Please enter a valid username");
            return false;
        }


        var data_ = { userName: userName_ };
           
        $.ajax({
            type: "POST",
            url: "AddKids.aspx/GetParentDetails",
            dataType: "json",
            data: JSON.stringify(data_),
            contentType: "application/json; charset=utf-8",
            async: true,
            cache: true,
            success: function (data) {
                var datafromServer = data.d.split(String.fromCharCode(31));
                var relationShip = "";

                $.each(datafromServer, function (i, v) {

                    var val = v.split(String.fromCharCode(30));

                    if( val != null && val[0] == "FATHER_NAME")
                        $('#mainContent_txtFatherName').val(val[1]);

                    else if (val != null && val[0] == "FATHER_CONTACT_NO")
                        $('#mainContent_txtFatherContact').val(val[1]);

                    else if (val != null && val[0] == "MOTHER_NAME")
                        $('#mainContent_txtMotherName').val(val[1]);

                    else if (val != null && val[0] == "MOTHER_CONTACT_NO")
                        $('#mainContent_txtMotherContact').val(val[1]);

                    else if (val != null && val[0] == "RES_CONTACT_NO") {

                        $('#mainContent_txtContactNumber').val(val[1]);
                    }

                    else if (val != null && val[0] == "RESIDENCE") {
                        $('#mainContent_txtAddress').val(val[1]);
                    }

                    else if (val != null && val[0] == "RELATIONSHIP") {
                        relationShip = val[1];
                    }

                    else if (val != null && val[0] == "EMAIL") {

                        if (relationShip == "Mother") {
                            $('#mainContent_txtMotherEmail').val(val[1]);
                        }
                        else if (relationShip == "Father") {
                            $('#mainContent_txtFatherEmail').val(val[1]);
                        }
                    }

                    else if (val != null && val[0] == "USERNAME") {

                        if (relationShip == "Mother") {
                            $('#mainContent_txtMotherIFSUser').val(val[1]);
                        }
                        else if (relationShip == "Father") {
                            $('#mainContent_txtFatherIFSUser').val(val[1]);
                        }
                        relationShip = "";
                    }

                    //alert(val[0] + '=' + val[1]);
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
        return false;
    });
});
    

$(document).ready(function () {

    $("#mainContent_txtKidNameTag").focusout(function () {

        var nameTag_ = $('#mainContent_txtKidNameTag').val();
        //alert(nameTag_);
        var data_ = { nameTag: nameTag_ };

        $.ajax({
            type: "POST",
            url: "AddKids.aspx/CheckExistKidNameTag",
            data: JSON.stringify(data_),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                    
                return true;
            },
            error: function (xhr, status, error) {

                var err = JSON.parse(xhr.responseText);
                errorMessage = err.Message;

                alert(errorMessage);
                $('#mainContent_txtKidNameTag').val('');
                $('#mainContent_txtKidNameTag').focus();
                // $('#ContentPlaceHolderBody_lbSiteName').text(errorMessage);                       
            }
        });

        return false;
    });

});
//$('form').submit(function (e) {

//    if (!$('#mainContent_btSave').valid()) {
//        e.preventDefault();
//    }      
//});

