CodeMirror.commands.autocomplete = function(cm) {
    CodeMirror.showHint(cm, CodeMirror.hint.html);
}
window.onload = function() {
    editor = CodeMirror(document.getElementById("code"), {
        mode: "text/html",
        theme: "neonsyntax",
        lineWrapping: true,
        lineNumbers: true,
        styleActiveLine: true,
        matchBrackets: true,

        extraKeys: {
            "Ctrl-Space": "autocomplete"
        },
        value: "<!doctype html>\n<html>\n  " + document.documentElement.innerHTML + "\n</html>"
    });
};