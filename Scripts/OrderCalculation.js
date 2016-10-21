
//Working on the Order Index page......
///The calculation goes here.......


$(document).ready(function () {
    setRequestDate();
    var id = $('#CustomerCode').val();
    if(id!=null)
    {        
        GetPercentOfEmblem(id);
        //debugger;
        
    }
    debugger;
    //var val = $('#RushOrder').val();
    //if ($('#RushOrder').is(':checked'))
    //{
    //    //$('#txtRushOrderPercent').val("35.00%");
    //}
    //else
    //{
    //    $('#RushOrderPercent').val("");
    //}

});

Date.prototype.AddDays = function (days) {
    // alert('this.getDate() ' + this.getDate())
    // alert('this.days' + days)
    this.setDate(this.getDate() + parseInt(days));
    //alert('this' + this)
    return this;
};


function setRequestDate()
{
    debugger;
    $('#Terms').val('NET 30');
    $('#txtTerms').val('NET 30');
    TodayDate = new Date();

    var weekDay = TodayDate.getDay();
    var days = 0;
    switch (weekDay) {
        case 0:
            //sunday
            days = 21 + 2;
            break;
        case 1:
            days = 21 + 1;
            break;
        case 3:
            days = 28;
            break;
        case 4:
            days = 21 + 6
            break;
        case 5:
            days = 21 + 5
            break;
        case 6:
            days = 21 + 6
            break;
        case 7:
            days = 21 + 3;
            break;
    }
    TodayDate.AddDays(days);
    $('#RequestDate').val(TodayDate.toLocaleDateString());
}


$(document).on('change', '#CustomerCode', function () {

    var customerID = $(this).val();
   
    GetPercentOfEmblem(customerID);

});

function GetPercentOfEmblem(customerID)
{
    if (checkNull(customerID) != 0) {
        $.ajax({
            url: '/Orders/GetEmblemByCustomerID?customerID=' + customerID,
            success: function (data) {
                if (data != null) {
                    var selval = $('#PercentOfEmblem').val();
                    //if () {
                    //    //$('#cmbPercentOfEmblem option[value="' + selval + '"]').attr('selected', 'selected');
                    //}
                    var htmlString = "<option value>select--</option>";
                    for (i = 0; i < data.length; i++) {
                        if (selval != "" && data[i].ID == selval)
                        {
                            htmlString += "<option value='" + data[i].ID + "' selected>" + data[i].Name + "</option>";
                        }
                        else
                        {
                            htmlString += "<option value='" + data[i].ID + "'>" + data[i].Name + "</option>";
                        }

                        
                    }
                    //data.each(function (idx, element) {


                    //});
                    $('#cmbPercentOfEmblem').html(htmlString);
                    debugger;
                  
                    
                }
            },
            error: function (err) {
                alert(err.statusText);
            }

        });
    }
}

$(document).on('change', '#cmbPercentOfEmblem', function () {
    debugger;
    $("#PercentOfEmblem").val($(this).val());
    GetBase();
    
});

function GetBase() {
    debugger;
    var qunatity = checkNull($("#Quantity").val());
    var size = checkNull($("#Size").val());
    var pricelist = 0;
    var cookie = readCookie("PriceList");
    if(!isNaN(cookie))
    {
        pricelist = parseInt(cookie);
    }
        
        //checkNull(sessionStorage.getItem("Materialtypepercent"));
    var emblem = checkNull($("#PercentOfEmblem").val());
    
    var code=$('#CustomerCode').val();
   
    //if (qunatity != undefined && size != undefined && pricelist != undefined && emblem != undefined && qunatity != "" && size != "" && pricelist != "" && emblem != "") {
        $.ajax({
            url: '/Orders/GetBaseData',
            data: { quntity: qunatity, size: size, emblem: emblem, customerCode:code },
            type: "POST",
            success: function (data) {
                debugger;
                if (data != "-1")
                {
                    var final = (data + (parseFloat(data) * parseFloat(pricelist))).toFixed(2);
                    $('#BasePrice').val(final);
                    $('#txtBasePrice').val(final);
                    calculateEmblemPrice();
                }
                else
                {
                    alert('Customer Code not selected.');
                }

                
            },
            error: function (err) {
                alert(err.statusText);
            }

        });
   // }

}

function checkNull(data)
{
    if (data != null && data != undefined && data != "")
    {
        return data;
    }
    return 0;
}

$(document).on('change','#Length',function(){
    getSize();
});

$(document).on('change', '#Width', function () {
    getSize();
});


//$(document).on('change', '#Size', function () {
//    getSize();
//});

function round(value, step) {
    step || (step = 1.0);
    var inv = 1.0 / step;
    return Math.round(value * inv) / inv;
}
function getSize()
{
    debugger;
    var length = $('#Length').val();
    var width = $('#Width').val();
    var size=$('#Size').val();
    var vfract;
    if(checkValue(length))
    {
        length = 0;
    }
    if (checkValue(width))
    {
        width = 0;
    }
    if(checkValue(size))
    {
        size=0;
    }

    
        
        length = parseFloat(length);
        width = parseFloat(width);
        if(!isNaN(length) && !isNaN(width))
        {
            var size = (length + width) / 2;
            //to make value round to 1 position
            size = round(size,0.5);
            vfract=size-parseInt(size);
            if(vfract!=0)
            {
                if (vfract != 0.5)
                {
                    if(vfract)
                    switch(vfract)
                    {
                        case (vfract >= .126 && vfract <= 0.625):
                            size = parseInt(size) + 0.5;
                        case (vfract <= 0.125):
                            size = parseInt(size);
                        case (vfract <= 0.125):
                            size = parseInt(size) + 1;
                    }
                }
                
            }

            if(size<2)
            {
                size = 2;
            }

            $('#Size').val(size);
            GetBase();
        }

       


}





function checkValue(val)
{
    if(val == '' || val == undefined)
    {
        return true;
    }
    return false;
}

function GetPriceEach() {
   


}



$(document).on('change', '#TypePricePercent', function () {

    debugger;

    var value = $(this).val();

    if(value!=undefined && value!="")
    {
        $.ajax({
            url: "/Orders/GetMaterialTypeValue",
            data: { ID:value },
            Type: "GET",
            success: function (data) {
                if (data != "-1")
                {
                    sessionStorage.setItem("Materialtypepercent", data);
                    data = data * 100;
                    $('#TypePricePercentValue').val(data);
                    data += "%";
                    $('#txtTypePricePercentValue').val(data);
                    calculateEmblemPrice();
                   // GetBase();
                }
                
            },
            error: function (err) {

                alert(err.statusText);
            }


        });
    }




})



function calculateEmblemPrice()
{
    debugger;
    var quantity= $("#Quantity").val();
    var size = $("#Size").val();
    var backingPrice = $("#BackingPrice").val();
    var borderPrice = $("#TypePriceValue").val();
    var basePrice = $("#BasePrice").val();
    if (basePrice == parseFloat(0))
    {
        basePrice = parseFloat(1);
    }
    var materialPrice = $("#TypePricePercentValue").val();
    var SpceialAmount;
    var specialThread = $('#SpecialThreadValue').val();
    var TotalColors = $('#TotalColors').val();
    var deposit;
    var SampleCharge;
    var rushOrder = $('#RushOrderPercent').val();

    
    quantity = checkNull(quantity);
    size = checkNull(size);
    
    backingPrice = checkNull(backingPrice);
    backingPrice = accounting.unformat(backingPrice);
    borderPrice = checkNull(borderPrice);
    borderPrice = accounting.unformat(borderPrice);
    basePrice = checkNull(basePrice);
    materialPrice = checkNull(materialPrice);
    if (materialPrice !=0 && materialPrice.indexOf('%') > 0)
    {
        materialPrice = materialPrice.replace('%', '');
    }
    materialPrice = materialPrice / 100;
    specialThread = checkNull(specialThread);
    if (specialThread !=0 && specialThread.indexOf('%') > 0)
    {
        specialThread = specialThread.replace('%', '');
    }
    specialThread = specialThread / 100;
   
    TotalColors = checkNull(TotalColors);
    rushOrder = checkNull(rushOrder);
    if (rushOrder !=0 && rushOrder.indexOf('%') > 0)
    {
        rushOrder = rushOrder.replace('%', '');
    }    
    rushOrder = rushOrder / 100;


    //if (basePrice == 0)
    //{
    //    return;
    //}


    var baseTotal = basePrice * quantity;


    TotalColors = TotalColors - 9;
    if(TotalColors>0)
    {
        TotalColors = quantity * (TotalColors * .05);
    }
    else
    {
        TotalColors = 0;
    }



    materialPrice = baseTotal * materialPrice;
    specialThread = baseTotal * specialThread;
    backingPrice = baseTotal * backingPrice;
    borderPrice = baseTotal * borderPrice;

    Total = baseTotal + materialPrice + specialThread + backingPrice + borderPrice + TotalColors;
    rushOrder = Total * rushOrder;
    Total = Total + rushOrder;
    var orderTotal;
    if(SampleCharge==0)
    {
        orderTotal = Total - deposit;
    }
    else
    {
        orderTotal = SampleCharge - deposit;
    }

    var actutalPrice=0;
    if(quantity!=0)
    {
        actutalPrice = (Total / quantity).toFixed(2);

    }
    
    $('#PriceEach').val(actutalPrice);
    $('#txtPriceEach').val(actutalPrice);
}

$(document).on('change', '#TypePrice', function () {
    debugger;
    var data = $(this).val();
    if(data!=undefined && data!="")
    {
        $.ajax({
            url: '/Orders/GetBorderType',
            data: { BType: data },
            success: function (data) {
                data = accounting.formatMoney(data);
                $('#TypePriceValue').val(data);
                calculateEmblemPrice();
            },
            error: function (err) {
                alert(err.statusText);
            }

        });
    }

})

$(document).on('change', '#Backing', function () {
    debugger;
    var quantity = $('#Quantity').val();
    var size = $("#Size").val();
    var backingType = $('#Backing').val();

    $.ajax({
        url: "/Orders/GetBackingPrice",
        data: { Size: size, BackingType: backingType },
        success: function (data) {
            sessionStorage.setItem("BackingPrice", data);
            if(data>0)
            {
                debugger;
                data = accounting.formatMoney(data);
                $('#BackingPrice').val(data);
                calculateEmblemPrice();
            }
            else
            {
                $('#BackingPrice').val("");
            }
        },
        error: function (err) {
            alert(err.statusText);
        }
    });
})

$(document).on('change', '#SpecialThreadValue', function () {
    debugger;
    var data = $(this).val();
    if (data.indexOf('.') < 0)
    {
        data = data + ".00";
    }
    if (data.indexOf('%') < 0)
    {
        
        data = data + "%";
    }
    //accounting.formatMoney(data, "%");

    $(this).val(data);
    calculateEmblemPrice();
});

$(document).on('change', '#RushOrder', function () {
    debugger;
    var data = 0;
    if($(this).is(':checked'))
    {
        data =35.00;
        $('#RushOrderPercent').val(data);
        data+="%";
        $('#txtRushOrderPercent').val(data);
        
    }
    else
    {
        $('#RushOrderPercent').val(data);
       // data += "%";
        $('#txtRushOrderPercent').val(data);
        //$('#RushOrderPercent').val(data);
    }
    calculateEmblemPrice();

})

$(document).on('change', '#Quantity', function () {
    GetBase();
});
