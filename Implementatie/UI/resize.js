window.onload = function() {
    resizeCourse()
};

function resizeCourse() {
    $(window).on('resize', function(){
        var footerHeight = $(".footer").outerHeight(true);
        var contenHeight = $(".content").height();
        $(".content").removeClass("h-100");
        $(".content").css("height", contenHeight - footerHeight*2);
    });
    console.log("resize")
}