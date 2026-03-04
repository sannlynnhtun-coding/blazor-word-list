window.spellCheck = {
    updateHighlights: function (elementId, misspelledWords) {
        const editor = document.getElementById(elementId);
        if (!editor) return;

        // Save cursor position
        const selection = window.getSelection();
        let offset = 0;
        if (selection.rangeCount > 0) {
            const range = selection.getRangeAt(0);
            const preRange = range.cloneRange();
            preRange.selectNodeContents(editor);
            preRange.setEnd(range.startContainer, range.startOffset);
            offset = preRange.toString().length;
        }

        const text = editor.innerText;
        let html = text;

        // Simple escaped regex for each misspelled word
        misspelledWords.forEach(word => {
            const regex = new RegExp(`(?<![\\w\\u1000-\\u109F])(${word})(?![\\w\\u1000-\\u109F])`, 'g');
            html = html.replace(regex, `<span class="squiggly cursor-pointer" data-word="$1">$1</span>`);
        });

        if (editor.innerHTML !== html) {
            editor.innerHTML = html;

            // Restore cursor position
            const newRange = document.createRange();
            const textNodes = Array.from(editor.childNodes)
                .flatMap(node => node.nodeType === 3 ? [node] : Array.from(node.childNodes))
                .filter(node => node.nodeType === 3);

            let currentOffset = 0;
            let found = false;
            for (const node of textNodes) {
                const length = node.textContent.length;
                if (currentOffset + length >= offset) {
                    newRange.setStart(node, offset - currentOffset);
                    newRange.setEnd(node, offset - currentOffset);
                    found = true;
                    break;
                }
                currentOffset += length;
            }

            if (found) {
                selection.removeAllRanges();
                selection.addRange(newRange);
            }
        }
    },
    initEditor: function (elementId, dotNetHelper) {
        const editor = document.getElementById(elementId);
        if (!editor) return;

        editor.addEventListener('click', function (e) {
            const span = e.target.closest('.squiggly');
            if (span) {
                const rect = span.getBoundingClientRect();
                const word = span.getAttribute('data-word');
                dotNetHelper.invokeMethodAsync('ShowSuggestions', word, rect.left, rect.bottom + window.scrollY);
            } else {
                dotNetHelper.invokeMethodAsync('HideSuggestions');
            }
        });
    },
    replaceWord: function (elementId, oldWord, newWord) {
        const editor = document.getElementById(elementId);
        if (!editor) return;

        // Simple replacement in HTML to preserve squigglies of other words
        // In a more robust impl, we'd search for the specific span
        const spans = editor.querySelectorAll('.squiggly');
        for (const span of spans) {
            if (span.getAttribute('data-word') === oldWord) {
                span.outerHTML = newWord; // Replace span with plain text
                break;
            }
        }
    },
    downloadText: function (filename, text) {
        const element = document.createElement('a');
        element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));
        element.setAttribute('download', filename);
        element.style.display = 'none';
        document.body.appendChild(element);
        element.click();
        document.body.removeChild(element);
    }
};
