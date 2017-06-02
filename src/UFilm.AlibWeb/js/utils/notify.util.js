define(['jquery', 'bs.notify'], function ($) {
    var vm = {};
    
    vm.success = function (message, title) {
        $.notify({
            title: title,
            message: message
        }, {
            placement: {
                from: "top",
                align: "center"
            },
            type: 'success'
        });
    }

    vm.error = function (message, title) {
        $.notify({
            title: title,
            message: message
        }, {
            placement: {
                from: "top",
                align: "center"
            },
            type: 'danger'
        });
    }

    vm.info = function (message, title) {
        $.notify({
            title: title,
            message: message
        }, {
            placement: {
                from: "top",
                align: "center"
            },
            type: 'info'
        });
    }

    return vm;
});
