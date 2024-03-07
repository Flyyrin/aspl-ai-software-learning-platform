window.onload = function() {
    editor = CodeMirror(document.getElementById("code"), {
        mode: "text/html",
        theme: "neonsyntax",
        lineWrapping: false,
        lineNumbers: true,
        styleActiveLine: true,
        matchBrackets: true,

        extraKeys: {
            "tab": "autocomplete"
        },
        value: "// Select a Excersice"
    });

    CodeMirror.commands.autocomplete = function(cm) {
        CodeMirror.showHint(cm, CodeMirror.hint.html);
    }
};