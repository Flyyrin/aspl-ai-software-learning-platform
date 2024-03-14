$(document).ready(function(){
    $('.chat-box').on("keypress", function(e) {
        if (e.keyCode == 13) {
            var question = $(".chat-box").val();
            $(".chat-box").val("");
            console.log(question);
            console.log("Respond");
            return false;
        }
    });
});