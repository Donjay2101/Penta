function deleteConfirm(ID,name,option,extra)
{
    if(confirm('you are about to delete the data? are you sure?'))
    {
        var url = '/' + option + '/Delete';
        $.ajax({
            url: url,
            data: { ID: ID },
            type: "POST",
            success: function (data) {
                url = '/' + option + '/' + option;
                if (extra != null)
                {
                    url += extra;
                }
               
                $('#dataContainer').load(url);


            },
            error: function (err) {
                alert(err.statusText);
            }

        });
    }



}

$(document).on('click', '#close', function () {

    $('#overLay').css('display', 'none');
    $('#overlayContainer').html('');
});

function goToPrevious(url)
{
    window.location.href = url;
}


function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
