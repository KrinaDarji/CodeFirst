$(document).ready(function () {
    GetDepartment();
    $('#emp').attr('disabled', true);
    $('#department').change(function () {
        $('#emp').attr('disabled', false);
        var id = $(this).val();
    
        var departmentId = $('#department').val();
        $('#emp').empty();
        $('#emp').append('<Option>--select employee--</Option>');
        $.ajax({
            url: '/Projects/getEmployeeManagerByDeptId?departmentId=' + departmentId,
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#emp').append('<Option value=' + data.id + '>' + data.employees.firstName + '</Option>');
                    console.log(data.rank);
                });
            }
        });
        $('#employees').empty();
        document.getElementById('empTbl').style.display = 'block';
        $.ajax({
            url: '/Managers/getEmployeeByDeptId?departmentId=' + departmentId,
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#employees').append('<tr> <td>' + data.departments.department_Name + '</td> <td>' + data.employees.firstName + '</td> </tr>');
                    console.log(data.rank);
                });
            }
        });
        $.ajax({
            url: '/Managers/Employee?id=' + id,
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#emp').append('<Option value=' + data.id + '>'+data.firstName+'</Option>');
                });
            }
        });
        $.ajax({
            url: '/Managers/Create?manager' + manager + '&departmentId=' + departmentId,
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#emp').append('<Option value=' + data.id + '>' + data.firstName + '</Option>');
                });
            }
        });
    });
});

function GetDepartment() {
    $.ajax({
        url: '/Managers/Department',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#department').append('<Option value=' + data.departmentId + '>' + data.department_Name + '</Option>');
            });
        }
    });
}
/*function SaveManager() {
   
        $.ajax({
            url: '/Managers/Create?id='  + id,
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#emp').append('<Option value=' + data.id + '>' + data.firstName + '</Option>');
                });
            }
        });
    });
}*/