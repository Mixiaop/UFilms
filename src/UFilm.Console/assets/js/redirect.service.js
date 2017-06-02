var redirectService = {};

redirectService.refresh = function (timeout) {
    setTimeout(function () { window.location.href = window.location.href; }, timeout);
}
redirectService.go = function (url) {
    window.location.href = url;
}