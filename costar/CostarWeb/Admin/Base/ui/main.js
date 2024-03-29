﻿jQuery(document).ready(function () {
    $("input.integer").numeric({ decimal: false });
    $("input.decimal").numeric();
    $("input.positive-integer").numeric({ decimal: false, negative: false });
    $("input.positive-decimal").numeric({ negative: false });
});

/*
 *
 * Copyright (c) 2006-2011 Sam Collett (http://www.texotela.co.uk)
 * Dual licensed under the MIT (http://www.opensource.org/licenses/mit-license.php)
 * and GPL (http://www.opensource.org/licenses/gpl-license.php) licenses.
 * 
 * Version 1.3.1
 * Demo: http://www.texotela.co.uk/code/jquery/numeric/
 *
 */
(function ($) { $.fn.numeric = function (config, callback) { if (typeof config === "boolean") { config = { decimal: config } } config = config || {}; if (typeof config.negative == "undefined") { config.negative = true } var decimal = (config.decimal === false) ? "" : config.decimal || "."; var negative = (config.negative === true) ? true : false; var callback = typeof callback == "function" ? callback : function () { }; return this.data("numeric.decimal", decimal).data("numeric.negative", negative).data("numeric.callback", callback).keypress($.fn.numeric.keypress).keyup($.fn.numeric.keyup).blur($.fn.numeric.blur) }; $.fn.numeric.keypress = function (e) { var decimal = $.data(this, "numeric.decimal"); var negative = $.data(this, "numeric.negative"); var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0; if (key == 13 && this.nodeName.toLowerCase() == "input") { return true } else { if (key == 13) { return false } } var allow = false; if ((e.ctrlKey && key == 97) || (e.ctrlKey && key == 65)) { return true } if ((e.ctrlKey && key == 120) || (e.ctrlKey && key == 88)) { return true } if ((e.ctrlKey && key == 99) || (e.ctrlKey && key == 67)) { return true } if ((e.ctrlKey && key == 122) || (e.ctrlKey && key == 90)) { return true } if ((e.ctrlKey && key == 118) || (e.ctrlKey && key == 86) || (e.shiftKey && key == 45)) { return true } if (key < 48 || key > 57) { var value = $(this).val(); if (value.indexOf("-") != 0 && negative && key == 45 && (value.length == 0 || ($.fn.getSelectionStart(this)) == 0)) { return true } if (decimal && key == decimal.charCodeAt(0) && value.indexOf(decimal) != -1) { allow = false } if (key != 8 && key != 9 && key != 13 && key != 35 && key != 36 && key != 37 && key != 39 && key != 46) { allow = false } else { if (typeof e.charCode != "undefined") { if (e.keyCode == e.which && e.which != 0) { allow = true; if (e.which == 46) { allow = false } } else { if (e.keyCode != 0 && e.charCode == 0 && e.which == 0) { allow = true } } } } if (decimal && key == decimal.charCodeAt(0)) { if (value.indexOf(decimal) == -1) { allow = true } else { allow = false } } } else { allow = true } return allow }; $.fn.numeric.keyup = function (e) { var val = $(this).value; if (val && val.length > 0) { var carat = $.fn.getSelectionStart(this); var decimal = $.data(this, "numeric.decimal"); var negative = $.data(this, "numeric.negative"); if (decimal != "") { var dot = val.indexOf(decimal); if (dot == 0) { this.value = "0" + val } if (dot == 1 && val.charAt(0) == "-") { this.value = "-0" + val.substring(1) } val = this.value } var validChars = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, "-", decimal]; var length = val.length; for (var i = length - 1; i >= 0; i--) { var ch = val.charAt(i); if (i != 0 && ch == "-") { val = val.substring(0, i) + val.substring(i + 1) } else { if (i == 0 && !negative && ch == "-") { val = val.substring(1) } } var validChar = false; for (var j = 0; j < validChars.length; j++) { if (ch == validChars[j]) { validChar = true; break } } if (!validChar || ch == " ") { val = val.substring(0, i) + val.substring(i + 1) } } var firstDecimal = val.indexOf(decimal); if (firstDecimal > 0) { for (var i = length - 1; i > firstDecimal; i--) { var ch = val.charAt(i); if (ch == decimal) { val = val.substring(0, i) + val.substring(i + 1) } } } this.value = val; $.fn.setSelection(this, carat) } }; $.fn.numeric.blur = function () { var decimal = $.data(this, "numeric.decimal"); var callback = $.data(this, "numeric.callback"); var val = this.value; if (val != "") { var re = new RegExp("^\\d+$|\\d*" + decimal + "\\d+"); if (!re.exec(val)) { callback.apply(this) } } }; $.fn.removeNumeric = function () { return this.data("numeric.decimal", null).data("numeric.negative", null).data("numeric.callback", null).unbind("keypress", $.fn.numeric.keypress).unbind("blur", $.fn.numeric.blur) }; $.fn.getSelectionStart = function (o) { if (o.createTextRange) { var r = document.selection.createRange().duplicate(); r.moveEnd("character", o.value.length); if (r.text == "") { return o.value.length } return o.value.lastIndexOf(r.text) } else { return o.selectionStart } }; $.fn.setSelection = function (o, p) { if (typeof p == "number") { p = [p, p] } if (p && p.constructor == Array && p.length == 2) { if (o.createTextRange) { var r = o.createTextRange(); r.collapse(true); r.moveStart("character", p[0]); r.moveEnd("character", p[1]); r.select() } else { if (o.setSelectionRange) { o.focus(); o.setSelectionRange(p[0], p[1]) } } } } })(jQuery);

/*
Auto-growing textareas; technique ripped from Facebook
(Textarea need set style "overflow:hidden" under IE)
*/
(function ($) {
    function times(string, number) {
        for (var i = 0, r = ''; i < number; i++) r += string;
        return r;
    }

    $.fn.autogrow = function (options) {
        this.filter('textarea').each(function () {
            this.timeoutId = null;
            var $this = $(this), minHeight = $this.height();
            var shadow = $('<div></div>').css({
                position: 'absolute',
                wordWrap: 'break-word',
                top: 0,
                left: -9999,
                display: 'none',
                width: $this.width(),
                fontSize: $this.css('fontSize'),
                fontFamily: $this.css('fontFamily'),
                lineHeight: $this.css('lineHeight')
            }).appendTo(document.body);

            var update = function () {
                var val = this.value.replace(/</g, '&lt;')
                  .replace(/>/g, '&gt;')
                  .replace(/&/g, '&amp;')
                  .replace(/\n$/, '<br/>&nbsp;')
                  .replace(/\n/g, '<br/>')
                  .replace(/ {2,}/g, function (space) { return times('&nbsp;', space.length - 1) + ' ' });
                shadow.html(val);
                $(this).css('height', Math.max(shadow.height(), minHeight));
            }

            var updateTimeout = function () {
                clearTimeout(this.timeoutId);
                var that = this;
                this.timeoutId = setTimeout(function () { update.apply(that); }, 100);
            };

            $(this).change(update).keyup(updateTimeout).keydown(updateTimeout);
            update.apply(this);
        });
        return this;
    }
})(jQuery);