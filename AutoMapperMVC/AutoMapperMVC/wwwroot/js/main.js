require.config({
    baseUrl: "/Scripts",
    paths: {
        jquery: "jquery-3.6.0.min"
    }
});

require(["jquery"], function ($) {
    $(function () {
        $("#todoList").append("<li></li>");
    });
});