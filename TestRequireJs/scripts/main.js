/// <reference path="jquery-2.1.1.intellisense.js" />
/// <reference path="jquery-2.1.1.js" />
require.config({
    //baseUrl: "/another/path",
    paths: {
        "jquery": "jquery-2.1.1"
    },
    map: {
        // '*' means all modules will get 'jquery-private'
        // for their 'jquery' dependency.
        '*': { 'jquery': 'jquery-private' },

        // 'jquery-private' wants the real jQuery module
        // though. If this line was not here, there would
        // be an unresolvable cyclic dependency.
        'jquery-private': { 'jquery': 'jquery' }
    }
});
require(["jquery"], function ($) {
    $("article > div").hide();
    $("article > h1").on("click", function () {
        $(this).parent("article").children("div").toggle();
    });
    $("#attachLink").on("click", function () {
        alert($.fn.jquery);
        return false;
    });
});
