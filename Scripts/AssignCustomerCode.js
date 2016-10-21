///------------------------------------Users Section--------------------------------------------------///

//Show pending Users-------------------------
$(document).on('click', '#pendingUsers', function () {
    LoadUsers(0);
    $('#activeUsers').removeClass('activeTab');
    $('#activeUsers').addClass('normalTab');
    $(this).removeClass('normalTab');
    $(this).addClass('activeTab');
});


//show Active Users---------------------------
$(document).on('click', '#activeUsers', function () {
    LoadUsers(1);
    $('#pendingUsers').removeClass('activeTab');
    $('#pendingUsers').addClass('normalTab');
    $(this).removeClass('normalTab');
    $(this).addClass('activeTab');
});


//common function to load customers------------------
function LoadUsers(option) {
    $('#dataContainer').load('/User/User?option=' + option);
}

//open Popup to assgin customer Code and Vendor MFG----------------------------------
$(document).on('click', '#assignCustomerCode', function () {

    $('#overLay').css('display', 'block');
    $('#overlayContainer').load('/User/AssignCustomerCode', function () {
        //var data=$('#UserID').html();
        //sessionStorage.setItem('PendingUsers', data);
        //var pricelist = $('#PriceList').html();
        //sessionStorage.setItem('PriceList',pricelist);
    });
});

//Add to List
$(document).on('click', '#btnAssign', function () {

  
    var userID = $('#UserID').val();
    var name;

    var editIndex = $('#editIndex').val();
    var editUserID = $('#editUserID').val();
    var id = $('#tempID').val();
    var tblArr = [];
    var tblData = sessionStorage.getItem('TblData');
    if (tblData != null) {
        tblArr = JSON.parse(tblData);
    }
    var useridx = searchinArray(tblArr, userID,'UserID');
    if (editIndex == "" || editIndex == undefined) {       
        if (useridx > -1) {
            alert('user already added to list.Click on edit to modify.');
            return;
        }
    }
    else
    {
        if (useridx > -1) {
            if(tblArr[useridx].UserID !=editUserID)
            {
                alert('user already added to list.Click on edit to modify.');
                return;
            }
            
        }
        
    }


    var customerCode = $('#customerCode').val();
    var PriceList = $('#PriceList').val();
    var VendorMFG = $('#VendorMFG').val();
    if (PriceList == '' || PriceList == undefined) {
        alert('Price list not selected');
        return;
    }

    if (VendorMFG == '' || VendorMFG == undefined) {
        alert('VendorMFG is not given to user.');
        return;
    }
    if (userID == undefined || userID == '') {

        alert('user ID is not selected');
        return;
    }
    name = $('#UserID option:selected').html();

    PLName = $('#PriceList option:selected').html();


    if (customerCode == undefined || customerCode == '') {
        alert('please enter customer code');
        return;
    }

    debugger;
  
    if (id == "" || id == undefined)
    {
        var no = sessionStorage.getItem("Length");
        if(isNaN(no))
        {
            no = 0;
        }
        id = no + 1;
    }

   
    var obj = new Object;
    obj.UserID = userID;
    obj.Username=name;
    obj.PriceListName=PLName;
    obj.Code = customerCode;
    obj.PriceList = PriceList;
    obj.VendorMFG = VendorMFG;
    obj.TempID = id;
    
    var idx = searchinArray(tblArr,id,'TempID');
    var newArr = addTolist(obj, idx, tblArr);
    appendTable(obj,editIndex);
    
    sessionStorage.setItem('TblData', JSON.stringify(newArr));
    sessionStorage.setItem('Length', newArr.length);
    clearControl();
});

//Edit to List
$(document).on('click', '.codeEdit', function () {

    var rowIndex = $(this).closest('tr').index();
    $('#editIndex').val(rowIndex);

    var tempID = $(this).closest('tr').attr('value');
    $('#tempID').val(tempID);

    var userID = $(this).closest('tr').find('td').eq(0).attr('value');
    $('#editUserID').val(userID);

    var tblArr = JSON.parse(sessionStorage.getItem('TblData'));
    if (tblArr != null)
    {
        var index = searchinArray(tblArr, tempID, 'TempID');
        $('#UserID').val(tblArr[index].UserID);
        $('#PriceList').val(tblArr[index].PriceList);
        $('#customerCode').val(tblArr[index].Code);
        $('#VendorMFG').val(tblArr[index].VendorMFG);
        $('#btnAssign').val("Save");
        $('#username').html(tblArr[index].Username);
        $('#editModel').css('display','block');

    }
    
});


//Delete from List
$(document).on('click', '.codeDelete', function () {
    debugger;
    if (confirm('you are about to delete a record from table are you sure?')) {
        var index = $(this).closest('tr').index();
        var tempID = $(this).closest('tr').attr('value');

        tblArr = JSON.parse(sessionStorage.getItem('TblData'));
        var idx = searchinArray(tblArr, tempID, 'TempID');
        var arr = tblArr.splice(idx, 1);
        sessionStorage.setItem('TblData', JSON.stringify(arr));
        $('#codeTable tr').eq(index).remove();
    }
});

//close edit mode of code
$(document).on('click', '#closeEdit', function () {
    clearControl();
});


//save data to server

$(document).on('click', '#btnSave', function () {

    debugger;
    var data = sessionStorage.getItem('TblData');


    //var serverData = JSON.parse(JSON.stringify(data));

    if (data != null) {
        $.ajax({
            url: '/user/UpdateCustomerCode',
            data: { Code:data},
            type: 'POST',
            success: function (data) {
                alert('data updated successfully');
                LoadUsers(1);                
                $("#codeTable").find("tr:gt(0)").remove();
            },
            error: function (err) {
                alert(err.statusText);
            }

        });
    }

});


//---------------------common functions-----------------------------------
function searchinArray(arr,item,searchProperty) {
    if (arr != null) {
        for (i = 0; i < arr.length; i++) {

            if (arr[i][searchProperty] == item) {
                return i;
            }
        }
    }
    return -1;
}

function addTolist(obj,indexNo, arr) {
    if (arr == null) {
        arr = [];
    }
    if (indexNo > -1) {
        arr[indexNo] = obj;
    }
    else {
        arr.push(obj);
    }
    return arr;
}

function clearControl()
{
    $('#UserID').val("");
    $('#PriceList').val("");
    $('#customerCode').val("");
    $('#VendorMFG').val("");

    $('#editIndex').val("");
    $('#tempID').val("");
    $('#editUserID').val("");
    $('#editModel').css('display','none');
    $('#btnAssign').val("Add to list");
}

function appendTable(obj,indexNo)
{
    if (indexNo != "" && indexNo>0)
    {
        $('#codeTable tr').eq(indexNo).find('td').eq(0).html(obj.Username);
        $('#codeTable tr').eq(indexNo).find('td').eq(0).attr('value', obj.UserID);
        $('#codeTable tr').eq(indexNo).find('td').eq(1).html(obj.Code);
        $('#codeTable tr').eq(indexNo).find('td').eq(2).html(obj.PriceList);
        $('#codeTable tr').eq(indexNo).find('td').eq(2).attr('value', obj.PriceListName);
        $('#codeTable tr').eq(indexNo).find('td').eq(3).html(obj.VendorMFG);

    }
    else
    {
        htmlString = "<tr value='"+obj.TempID+"'>";
        htmlString += "<td value='" + obj.UserID + "'>" + obj.Username + "</td>";
        htmlString += "<td>" + obj.Code+ "</td>";
        htmlString += "<td value='" + obj.PriceList + "'>" + obj.PriceListName+ "</td>";
        htmlString += "<td>" + obj.VendorMFG + "</td>";
        htmlString += "<td><span class='codeEdit' style='cursor:pointer'>Edit</span>|<span style='cursor:pointer' class='codeDelete'>Delete</span></td>";
        htmlString += "</tr>";
        $('#codeTable').append(htmlString);
    }
}

$(document).ready(function () {
    sessionStorage.clear();
});

//---------------------common functions-----------------------------------








