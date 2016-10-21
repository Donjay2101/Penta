$(document).ready(function () {

    var PriceList = sessionStorage.getItem('PriceListName');
    if(PriceList!=null)
    {
        $('#PriceListName').val(PriceList);
        var ID = sessionStorage.getItem('PriceListID');
        $('#PriceListID').val(ID);
        $('#divEmblem').css('display', 'block');

    }
    var Emblem = sessionStorage.getItem('Emblem');
    if(Emblem!=null)
    {
        debugger;
        $('#tblEmblem').html(Emblem);
        $('#divList').css('display', 'block');
        $('#divList').load("/PriceLists/PriceListNew?ID=" + ID);
    }
     



});


$(document).on('click', '#btnEmblemAdd', function () {
    debugger;
    var value = $('#EmblemValue').val();
    if (value != "" && value != undefined) {
        var str=generateString(value);
        $('#tblEmblem tbody').append(str);
    }
});


function generateString(value)
{
    var htmlString = "<tr>";
    htmlString += "<td>";
    htmlString += value;
    htmlString += "</td>";
    htmlString += "<td><span id='ebmEdit' style='cursor:pointer'>Edit</span> | <span style='cursor:pointer' id='ebmDelete'>Delete</span></td>";
    htmlString += "</tr>";

    return htmlString;
}

$(document).on('click', '#ebmEdit', function () {

    var idx = $(this).closest('tr').index();
    var value = $('#tblEmblem tbody').find('tr').eq(idx).find('td').eq(0).html();
    var htmlString = "";
    htmlString += "<td><input type='text' value='"+value+"' id='txtEditEmblem'/></td>";
    htmlString += "<td><span id='ebmSave' style='cursor:pointer'>Save</span> | <span style='cursor:pointer' id='ebmClose'>Close</span></td>";
    //htmlString += "</tr>";

    var preRow = sessionStorage.setItem('PreRow', $('#tblEmblem tbody').find('tr').eq(idx).html());
    $('#tblEmblem tbody').find('tr').eq(idx).html(htmlString);
});

$(document).on('click', '#ebmDelete', function () {

    $(this).closest('tr').remove();
});

$(document).on('click', '#ebmSave', function () {
    debugger;
    var value = $('#txtEditEmblem').val();
    var idx = $(this).closest('tr').index();
    var str=""; 
    if (value != "" && value != undefined)
    {
        htmlString = "<td>";
        htmlString += value;
        htmlString += "</td>";
        htmlString += "<td><span id='ebmEdit' style='cursor:pointer'>Edit</span> | <span style='cursor:pointer' id='ebmDelete'>Delete</span></td>";
       $(this).closest('tr').html(htmlString);
        //$('#tblEmblem tbody').find('tr').eq(idx).html(htmlString);
    }
    
   
});


$(document).on('click', '#ebmClose', function () {
    debugger;
    var value = sessionStorage.getItem('PreRow');
    var str = value;

    $(this).closest('tr').html(value);
       
});


$(document).on('click', '#btnPriceListNSave', function () {
     
    
    var name = $('#PriceListName').val();
    var ID = $('#PriceListID').val();
    if (ID == "")
    {
        ID = 0;
        
    }
    url = "/PriceLists/SavePriceListName";
    //else
    //{
    //    ID = 0;
    //    url = "/PriceLists/SavePriceListName";
    //}
    
    if (name != "") {
        $.ajax({
            url: url,
            data: {ID:ID,Name: name },
            type: "POST",
            success: function (data) {
                if (data == -1) {
                    alert('Name already present in database');
                }
                else if (data > 0) {
                    $('#PriceListID').val(data);
                    sessionStorage.setItem('PriceListName', name);
                    sessionStorage.setItem('PriceListID', data);
                    $('#divEmblem').css('display', 'block');
                }
            },
            error: function (err) {
                alert(err.statusText);
            }



        });

    }
    
})


$(document).on('click', '#btnEmblemSave', function () {

    debugger;
    var arr = new Array;
    var len = $('#tblEmblem tbody tr').length;
    var PriceListID = $('#PriceListID').val();
    if (PriceListID != "")
    {
        if (len > 0) {
            for (i = 0; i < len; i++) {
                var obj = new Object;
                var value = $('#tblEmblem tbody tr').eq(i).find('td').eq(0).html();
                obj.value = value;
                obj.PriceList = PriceListID;
                arr.push(obj);
            }

            var data = JSON.stringify(arr);

            $.ajax({
                url: '/PriceLists/SaveEmblems',
                data: { model: data },
                dataType:"JSON",
                type: 'POST',
                ContentType: "Appliation/JSON",
                success: function (data) {
                        if (data == "1")
                        {

                            $('#divList').css('display', 'block');
                            var tbl=$('#tblEmblem').html();
                            sessionStorage.setItem('Emblem', tbl);

                            $('#divList').load("/PriceLists/PriceListNew?ID=" + PriceListID);
                        }
                        else{
                            alert('something went wrong try again');
                        }

                    },
                error: function (err) {
                    alert(err.statusText);
                }


            });
        }

        
    }
    

});


$(document).on('click', '#btnListAdd', function () {
    debugger;
    var listarr = new Array;
    var size = $('#Size').val();
    var first = $('#First').val();
    var Second = $('#Second').val();
    var third = $('#Third').val();
    var fourth = $('#Fourth').val();
    var fifth = $('#Fifth').val();
    var sixth = $('#Sixth').val();
    var seventh = $('#Seventh').val();
    var Eighth = $('#Eighth').val();
    var EmblemID = $('#Emblem').val();
    var PriceListID = $('#PriceListID').val();
    var EmblemName = $('#Emblem option:selected').html();
    var EmblemList = $('#Emblem').html();
    sessionStorage.setItem("EmblemList",EmblemList);
    var htmlString = "<tr>";
    if (size != "" && first != "" && Second != "" && third != "" && fourth != "" && fifth != "" && sixth != "" && seventh != "" && Eighth != "") {
        htmlString += generateHtml(size, first, Second, third, fourth, fifth, sixth, seventh, Eighth,EmblemName,EmblemID);
        htmlString += "</tr>";
        $('#tblList tbody').append(htmlString);
    }
    var str=JSON.parse(sessionStorage.getItem('ListArray'));
    if(str!=null)
    {
        listarr =str ;
    }
    
    //listarr=
    var Listobj = new Object;
    Listobj.Size = size;
    Listobj.Emblem = EmblemID;
    Listobj.First = first;
    Listobj.Second = Second;
    Listobj.Third= third;
    Listobj.Fourth= fourth;
    Listobj.Fifth= fifth;
    Listobj.Sixth= sixth;
    Listobj.Seventh = seventh;
    Listobj.Eighth = Eighth;
    Listobj.PriceListID = PriceListID;

    listarr.push(Listobj);
    var arr = JSON.stringify(listarr);
    sessionStorage.setItem('ListArray', arr);

});

function generateHtml(size,first,second,third,fourth,fifth,sixth,seventh,eighth,EmblemName,EmblemID)
{
    var htmlString = "<td>" + size + "</td>";
    htmlString += "<td value='"+EmblemID+"'>" + EmblemName+ "</td>";
    htmlString += "<td>" + first + "</td>";
    htmlString += "<td>" + second+ "</td>";
    htmlString += "<td>" + third+ "</td>";
    htmlString += "<td>" + fourth + "</td>";
    htmlString += "<td>" + fifth + "</td>";
    htmlString += "<td>" + sixth + "</td>";
    htmlString += "<td>" + seventh + "</td>";
    htmlString += "<td>" + eighth+ "</td>";
    htmlString += "<td><span id='liEdit' style='cursor:pointer'>Edit</span> | <span id='liDelete' style='cursor:pointer'>Delete</span></td>";
    return htmlString;
}

$(document).on('click', '#liEdit', function () {
    var idx = $(this).closest('tr').index();

    var size = $(this).closest('tr').find('td').eq(0).html();
    var EmblemID = $(this).closest('tr').find('td').eq(1).attr('value');
    var first = $(this).closest('tr').find('td').eq(2).html();
    var second = $(this).closest('tr').find('td').eq(3).html();
    var third = $(this).closest('tr').find('td').eq(4).html();
    var fourth = $(this).closest('tr').find('td').eq(5).html();
    var fifth = $(this).closest('tr').find('td').eq(6).html();
    var sixth = $(this).closest('tr').find('td').eq(7).html();
    var seventh = $(this).closest('tr').find('td').eq(8).html();
    var eighth = $(this).closest('tr').find('td').eq(9).html();
    
    var EmblemList="<select id='eEmblem'>"+sessionStorage.getItem('EmblemList')+"</select>";
    var htmlString = "<td><input type='text' id='eSize' value='" + size + "'/></td>";
    htmlString += "<td>" + EmblemList + "</td>";
    htmlString += "<td> <input type='text' id='efirst' value='" + first + "'/></td>";
    htmlString += "<td> <input type='text' id='esecond' value='" + second + "'/></td>";
    htmlString += "<td> <input type='text' id='ethird'  value='" + third + "'/></td>";
    htmlString += "<td> <input type='text' id='efourth' value='" + fourth + "'/></td>";
    htmlString += "<td> <input type='text' id='efifth' value='" + fifth + "'/></td>";
    htmlString += "<td> <input type='text' id='esixth' value='" + sixth + "'/></td>";
    htmlString += "<td><input type='text' id='eseventh' value='" + seventh + "'/></td>";
    htmlString += "<td> <input type='text' id='eeighth' value='" + eighth + "'/></td>";
    htmlString += "<td><span id='liSave' style='cursor:pointer'>Save</span> | <span id='liClose' style='cursor:pointer'>Close</span></td>";
    sessionStorage.setItem('PreLiRow', $(this).closest('tr').html());
    $(this).closest('tr').html(htmlString);
    $('#eEmblem').find('option[value='+EmblemID+']').attr('selected','selecetd');
   
    
});

$(document).on('click', '#liDelete', function () {

    var idx = $(this).closest('tr').index();
    var listarr = new Array();
    var str = sessionStorage.getItem('ListArray');
    listarr = JSON.parse(str);
    listarr.splice(idx, 1);
    $(this).closest('tr').remove();
});


$(document).on('click', '#liSave', function () {

    debugger;
    var idx = $(this).closest('tr').index();
    var size = $('#eSize').val();
    var EmblemID = $('#eEmblem').val();
    var EmblemName = $('#eEmblem option:selected').html();
    var first = $('#efirst').val();
    var Second = $('#esecond').val();
    var third = $('#ethird').val();
    var fourth = $('#efourth').val();
    var fifth = $('#efifth').val();
    var sixth = $('#esixth').val();
    var seventh = $('#eseventh').val();
    var Eighth = $('#eeighth').val();
    var PriceListID = $("#PriceListID").val();
    var htmlString = "";
    if (size != "" && first != "" && Second != "" && third != "" && fourth != "" && fifth != "" && sixth != "" && seventh != "" && Eighth != "") {
        htmlString += generateHtml(size, first, Second, third, fourth, fifth, sixth, seventh, Eighth,EmblemName,EmblemID);

        $(this).closest('tr').html(htmlString);

        debugger;
        var listarr = new Array();
        var str = sessionStorage.getItem('ListArray');
        listarr = JSON.parse(str);
        var Listobj = new Object;
        Listobj.Size = size;
        Listobj.Emblem = EmblemID;
        Listobj.First = first;
        Listobj.Second = Second;
        Listobj.Third = third;
        Listobj.Fourth = fourth;
        Listobj.Fifth = fifth;
        Listobj.Sixth = sixth;
        Listobj.Seventh = seventh;
        Listobj.Eighth = Eighth;
        Listobj.PriceListID = PriceListID;

        listarr.splice(idx, 0, Listobj)
        str = JSON.stringify(listarr);
        sessionStorage.setItem('ListArray', str);
    };

});

$(document).on('click', '#liClose', function () {

    var value = sessionStorage.getItem('PreLiRow');

    $(this).closest('tr').html(value);
})



$(document).on('click', '#btnSaveList', function () {
    debugger;
    var listarr = new Array();
    var str = sessionStorage.getItem('ListArray');

    if (str != null)
    {
        listarr=JSON.parse(str);
        $.ajax({
            url: '/PriceLists/SavePriceList',
            data: { model:JSON.stringify(listarr)},
            type: "POST",            
            success: function (data) {

            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    }
    


    

});















