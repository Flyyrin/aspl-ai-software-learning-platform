window.onload = function() {
    $(".resizable").resizable({
        handles: "n", // Only allow resizing from the top edge
        minHeight: 100 // Set minimum height to avoid collapsing
    });
    $( ".resizable" ).resizable();
    console.log("resize")
};