var notify = {};
notify.success = function success(message,title) {
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

notify.error = function success(message, title) {
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

//
var page = {};
page.reload = function (time) {
    if (time > 0) {
        setTimeout(function () {
            window.location.href = window.location.href;
        }, time);
    } else {
        window.location.href = window.location.href;
    }
}