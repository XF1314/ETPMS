
kendo.ui.plugin(kendo.ui.Window.extend({
    // set options
    options: {
        name: 'GDialog',    // widget name
        onOk: $.noop,
        okText: '确定',
        cancelText: '取消',
        defaultButtons: 'OK_CANCEL',  //'OK' || 'OK_CANCEL' || 'CANCEL' || 'NULL'
        extraButtons: [],           //[ { text:'ok', className: '', click:function(){} }]
        appendTo: 'body',
        minWidth: 400,
        minHeight: 100,
        resizable: false,
        actions: ['Close'],            // ['Minimize', 'Maximize', 'Close']
        autohide: false,
        time: 1000
    },
    // Init method
    init: function (element, options) {
        var me = this;
        // Call super.init
        kendo.ui.Window.fn.init.call(this, element, options);

        // Add buttons
        var $buttonsArea = this._addButtons(element, options);

        // Set styles
        this.wrapper.addClass('k-dialog');
        $buttonsArea.addClass('k-button-area');

        // Set the dialog's position by default
        if (!options || !options.position) { this.center(); }

        // if the autohide is setted true that aftering a time auto hide the dialog. default is 1s.
        if (this.options.autohide) {
            setTimeout(function () {
                kendo.ui.Window.fn.close.call(me);
            }, this.options.time);
        }
    },
    open: function () {
        // Call super.open
        kendo.ui.Window.fn.open.call(this);
    },
    minimize: function () {
        // Call super.minimize
        kendo.ui.Window.fn.minimize.call(this);

        $(this.buttonsArea).hide();
        this.wrapper.css('padding-bottom', '0');
    },
    restore: function () {
        // Call super.restore
        kendo.ui.Window.fn.restore.call(this);

        $(this.buttonsArea).show();
        this.wrapper.css('padding-bottom', '51px');
    },
    center: function () {

        if (this.options.isMaximized) { return this; }

        // Call super.center
        kendo.ui.Window.fn.center.call(this);

        var that = this,
            position = that.options.position,
            wrapper = that.wrapper,
            documentWindow = $(window),
            scrollTop = 0,
            newTop;

        if (!that.options.pinned) { scrollTop = documentWindow.scrollTop(); }

        newTop = scrollTop + Math.max(0,
                (documentWindow.height() - wrapper.height() - 50 - parseInt(wrapper.css("paddingTop"), 10)) / 2);

        wrapper.css({ top: newTop });

        position.top = newTop;

        return that;
    },
    _onDocumentResize: function () {
        if (!this.options.isMaximized) { return; }

        // Call super._onDocumentResize
        kendo.ui.Window.fn._onDocumentResize.call(this);

        this._fixWrapperHeight();
    },
    _fixWrapperHeight: function () {
        var height = (this.wrapper.height() - 51).toString() + 'px';
        this.wrapper.css('height', height);
    },
    // Add buttons to the dialog
    _addButtons: function (element, options) {

        var that = this,
            buttons = this.options.extraButtons.slice(0);

        var nullPattern = /NULL/gi, okPattern = /OK/gi, cancelPattern = /CANCEL/gi,
            defaultButtons = {
                buttonOk: {
                    text: that.options.okText, className: 'ok-btn', click: function (e) {
                        that.options.onOk.call(that, e);
                        return false;
                    }
                },
                buttonCancel: {
                    text: that.options.cancelText, className: 'close-btn', click: function (e) {
                        e.preventDefault(); that.close();
                    }
                }
            };

        // Append default buttons
        if (!nullPattern.test(this.options.defaultButtons)) {
            okPattern.test(this.options.defaultButtons) &&
               buttons.unshift(defaultButtons.buttonOk);
            cancelPattern.test(this.options.defaultButtons) &&
               buttons.unshift(defaultButtons.buttonCancel);
        }

        // Make button area
        var buttonsArea = document.createElement('div'),
            $buttonsArea = $(buttonsArea);
        this.buttonsArea = buttonsArea;

        // Make buttons and set buttons' attributes
        for (var i = buttons.length - 1; i >= 0; --i) {
            var $aEl = $(document.createElement('a'));
            /*eslint no-script-url: 0*/
            $aEl.html(buttons[i].text)
                .addClass('k-dialog-button')
                .addClass(buttons[i].className)
                .attr({ href: 'javascript:;' })
                .on('click', buttons[i].click)
                .kendoButton();
            $buttonsArea.append($aEl);
        }

        this.wrapper.append($buttonsArea);

        return $buttonsArea;
    }
}));