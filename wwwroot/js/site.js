// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ShowAdd() {
    getList(Number($('.table tr').last().find('td').first().html()), $('input[name=search]').val());
}

function getList(lastId = 1, search = '') {
    $.ajax({
        type: "POST",
        url: "/API/GetListJobers/",
        data: {
            lastId: lastId,
            search: search
        },
        dataType: 'html',
        success: function (responce) {
            if (typeof $(responce).find('.bodyQuery tbody').html() == "undefined") {
                $('.showAdd').remove();
                return;
            }
            $('.table tbody').append($(responce).find('.bodyQuery tbody').html());
            if (typeof $('.showAdd').get(0) == 'undefined') {
                $('.table').after($('<button>', {
                    'class': 'showAdd',
                    'onclick': 'ShowAdd();',
                    text: 'Показать ещё'
                }));
            }
            init();
        },
        error: function (errors) {
            console.log(errors);
        },
    });
}

function getListDepartments(search = '') {
    if (search == '') {
        var url = "/API/GetDepartments/";
        var data = {};
    } else {
        var url = "/API/GetDepartmentsSearch/";
        var data = { search: search };
    }
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        dataType: 'html',
        success: function (responce) {
            $('.departments-list').html($(responce).html());
            init();
        },
        error: function (errors) {
            console.log(errors);
        },
    });
}

function AddJober() {
    $('.popup-body').html('');
    $.ajax({
        type: "POST",
        url: "/API/FormAddJober/",
        dataType: 'html',
        success: function (responce) {
            $('.popup-body').append(responce);
            $('.popup-fade').fadeIn();
            GetDepartmentsList();
            init();
        },
        error: function (errors) {
            console.log(errors);
        },
    });
}

function AddDepartment() {
    $('.popup-body').html('');
    $.ajax({
        type: "POST",
        url: "/API/FormAddDepartment/",
        dataType: 'html',
        success: function (responce) {
            $('.popup-body').append(responce);
            $('.popup-fade').fadeIn();
            GetDepartmentsList(false);
            init();
        },
        error: function (errors) {
            console.log(errors);
        },
    });
}

function deleteJober(id) {
    $.ajax({
        type: "POST",
        url: "/API/DeleteJober/",
        data: {
            'id': id
        },
        success: function () {
            $('.table tbody').html('');
            getList();
        },
        error: function (errors) {
            console.log(errors);
        },
    });
}

function deleteDepartment(id) {
    $.ajax({
        type: "POST",
        url: "/API/DeleteDepartment/",
        data: {
            'id': id
        },
        success: function () {
            getListDepartments();
        },
        error: function (errors) {
            console.log(errors);
        },
    });
}

function GetDepartmentsList(flag = true, departmentId = 0) {
    $.ajax({
        type: "POST",
        url: "/API/GetDepartmentsList/",
        data: {
            departmentId: departmentId
        },
        dataType: 'html',
        success: function (responce) {
            $('.popup-body td.departments').html(responce)
            if (flag) {
                GetPositionsList($(responce).val()) 
            }
            init();
        },
        error: function (errors) {
            console.log(errors);
        },
    });
}

function GetPositionsList(departmentId) {
    $.ajax({
        type: "POST",
        url: "/API/GetPositionsList/",
        data: {
            departmentId: departmentId
        },
        dataType: 'html',
        success: function (responce) {
            $('.popup-body td.positions').html(responce)
        },
        error: function (errors) {
            console.log(errors);
        },
    });
}

function UpdateDepartment(id) {
    $('.popup-body').html('');

    $.ajax({
        type: "POST",
        url: "/API/FormUpdateDepartment/",
        data: {
            id: id
        },
        dataType: 'html',
        success: function (responce) {
            $('.popup-body').append(responce);
            $('.popup-fade').fadeIn();
            GetDepartmentsList(false, id);
            init();
        },
        error: function (errors) {
            console.log(errors);
        },
    });
}

function init() {
    $(":input").inputmask();
    $('form[name=login]').unbind().on('submit', function (e) {
        e.preventDefault();
        let $this = $(this);
        var formData = new FormData($this.get(0));
        $.ajax({
            type: "POST",
            url: "/API/Authorization/",
            data: formData,
            contentType: false,
            processData: false,
            success: function (responce) {
                responce = JSON.parse(responce);
                if (responce.success) {
                    document.cookie = "user=" + responce.success + ";max-age=3600";
                } else if (responce.error) {
                    $this.before($('<p>', { text: responce.error }));
                }
            },
            error: function (errors) {
                console.log(errors);
            },
        });
    })
    $('form[name=update]').unbind().on('submit', function (e) {
        e.preventDefault();
        let $this = $(this);
        var formData = new FormData($this.get(0));
        $.ajax({
            type: "POST",
            url: "/API/UpdateJober/",
            data: formData,
            contentType: false,
            processData: false,
            success: function () {
                $('.popup-fade').fadeOut();
                setTimeout(() => {
                    $('.popup-body').html('');
                    $('.table tbody').html('');
                    getList();
                }, 400);
            },
            error: function (errors) {
                console.log(errors);
            },
        });
    })
    $('form[name=updateDepartment]').unbind().on('submit', function (e) {
        e.preventDefault();
        let $this = $(this);
        var formData = new FormData($this.get(0));
        $.ajax({
            type: "POST",
            url: "/API/UpdateDepartment/",
            data: formData,
            contentType: false,
            processData: false,
            success: function () {
                $('.popup-fade').fadeOut();
                setTimeout(() => {
                    $('.popup-body').html('');
                    getListDepartments();
                }, 400);
            },
            error: function (errors) {
                console.log(errors);
            },
        });
    })

    $('form[name=add]').unbind().on('submit', function (e) {
        e.preventDefault();
        let $this = $(this);
        var formData = new FormData($this.get(0));
        $.ajax({
            type: "POST",
            url: "/API/AddJober/",
            data: formData,
            contentType: false,
            processData: false,
            success: function () {
                $('.popup-fade').fadeOut();
                setTimeout(() => {
                    $('.popup-body').html('');
                    $('.table tbody').html('');
                    getList();
                }, 400);
            },
            error: function (errors) {
                console.log(errors);
            },
        });
    })

    $('form[name=addDepartment]').unbind().on('submit', function (e) {
        e.preventDefault();
        let $this = $(this);
        var formData = new FormData($this.get(0));
        $.ajax({
            type: "POST",
            url: "/API/AddDepartment/",
            data: formData,
            contentType: false,
            processData: false,
            success: function () {
                $('.popup-fade').fadeOut();
                setTimeout(() => {
                    $('.popup-body').html('');
                    getListDepartments();
                }, 400);
            },
            error: function (errors) {
                console.log(errors);
            },
        });
    })

    $('.table tbody td').unbind().click(function () {
        if ($(this).attr('name') == 'buttonDelete') return;
        $('.popup-body').html('');

        $.ajax({
            type: "POST",
            url: "/API/FormUpdateJober/",
            data: {
                id: $(this).parent('tr').data('id')
            },
            dataType: 'html',
            success: function (responce) {
                $('.popup-body').append(responce);
                $('.popup-fade').fadeIn();
                GetDepartmentsList();
                init();
            },
            error: function (errors) {
                console.log(errors);
            },
        });
    });
    $('.select-depatrment-id').unbind().on('change', function () {
        GetPositionsList($(this).val())
    });
}

$(function () {
    init();

    $('input[name=search]').unbind().on('input', function () {
        let $this = $(this);
        let intputVal = $(this).val();
        setTimeout(() => {
            if ($this.val() == intputVal) {
                $('.table tbody').html('');
                getList(1, intputVal);
            }
        }, 1000);
    });

    $('input[name=searchDepartments]').unbind().on('input', function () {
        let $this = $(this);
        let intputVal = $(this).val();
        setTimeout(() => {
            if ($this.val() == intputVal) {
                getListDepartments(intputVal);
            }
        }, 1000);
    });

    $('.popup-close').unbind().click(function () {
        $(this).parents('.popup-fade').fadeOut();
        setTimeout(() => {
            $('.popup-body').html('');
        }, 400);
    });

    $('.popup-fade').unbind().click(function (e) {
        if ($(e.target).closest('.popup').length == 0) {
            $(this).fadeOut();
            setTimeout(() => {
                $('.popup-body').html('');
            }, 400);
        }
    });
});