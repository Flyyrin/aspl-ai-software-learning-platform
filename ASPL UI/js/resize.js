window.onload = function() {
    resizeCourse()
};

$(window).on("resize", function() {
    resizeCourse()
});

function resizeCourse() {
    windowHeight = $(window).height()
    headerHeight = $(".header").outerHeight();
    footerHeight = $(".question-section").outerHeight();
    $(".course").height(windowHeight - headerHeight - footerHeight)
}