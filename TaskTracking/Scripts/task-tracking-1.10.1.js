/// <reference path="jquery-1.10.2.min.js" />
/// <reference path="bootstrap.min.js" />
$(document).ready(function () {

    $('#spanUsername').text('Hello ' + localStorage.getItem('userName'));

    if (localStorage.getItem('accessToken') == null) {
        window.location.href = 'Login.html';
    }

    $('#btnLogout').click(function () {
        localStorage.removeItem('accessToken');
        window.location.href = 'Login.html';
    });

    loadDataInit();

    // load tasks init
    function loadDataInit() {
        $.ajax({
            url: 'api/Task/GetAll',
            method: 'GET',
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
            },
            success: function (data) {
                $('#divData').removeClass('hidden');
                $('#tblBody').empty();
                $.each(data, function (index, value) {
                    var row = $('<tr><td>'
                        +'<a href="#">'+ value.task_id+ '</a>' + '</td><td>'
                        + value.subject + '</td><td>'
                        + value.description + '</td><td>'
                        + value.priority + '</td><td>'
                        + value.type + '</td><td>'
                        + value.status + '</td></tr>');
                    $('#tblData').append(row);

                });
            },
            error: function (jqXHR) {
                if (jqXHR.status == "401") {
                    $('#errorModel').modal('show');
                } else {
                    $('#divErrorText').text(jqXHR.responseText);
                    $('#divError').show('fade');
                }
            }
        });
    }
});