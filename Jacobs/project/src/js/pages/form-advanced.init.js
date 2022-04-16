
/**
* Theme:  Moltran - Responsive Bootstrap 4 Admin & Dashboard
* Author: Coderthemes
* File:   Form advanced
*/
!function($) {
    "use strict";

    var Components = function() {};


    //switch
    Components.prototype.initSwitchery = function() {
        $('[data-plugin="switchery"]').each(function (idx, obj) {
            new Switchery($(this)[0], $(this).data());
        });
    },

    //Max Length
    Components.prototype.initMultiSelect = function() {

        $('[data-plugin="multiselect"]').multiSelect($(this).data());

        $('#my_multi_select3').multiSelect({
            selectableHeader: "<input type='text' class='form-control search-input' autocomplete='off' placeholder='search...'>",
            selectionHeader: "<input type='text' class='form-control search-input' autocomplete='off' placeholder='search...'>",
            afterInit: function (ms) {
                var that = this,
                    $selectableSearch = that.$selectableUl.prev(),
                    $selectionSearch = that.$selectionUl.prev(),
                    selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable:not(.ms-selected)',
                    selectionSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selection.ms-selected';
    
                that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
                    .on('keydown', function (e) {
                        if (e.which === 40) {
                            that.$selectableUl.focus();
                            return false;
                        }
                    });
    
                that.qs2 = $selectionSearch.quicksearch(selectionSearchString)
                    .on('keydown', function (e) {
                        if (e.which == 40) {
                            that.$selectionUl.focus();
                            return false;
                        }
                    });
            },
            afterSelect: function () {
                this.qs1.cache();
                this.qs2.cache();
            },
            afterDeselect: function () {
                this.qs1.cache();
                this.qs2.cache();
            }
        });
    }

    //Select2
    Components.prototype.initSelect2 = function() {
        // Select2
        $('[data-toggle="select2"]').select2();
    },

    // Inputmask
    Components.prototype.initInputmask = function() {
        $('[data-toggle="input-mask"]').each(function (idx, obj) {
            var maskFormat = $(obj).data("maskFormat");
            var reverse = $(obj).data("reverse");
            if (reverse != null)
                $(obj).mask(maskFormat, {'reverse': reverse});
            else
                $(obj).mask(maskFormat);
        });
    },

    // touchspin
    Components.prototype.initTouchspin = function() {
        var defaultOptions = {
        };

        // touchspin
        $('[data-toggle="touchspin"]').each(function (idx, obj) {
            var objOptions = $.extend({}, defaultOptions, $(obj).data());
            $(obj).TouchSpin(objOptions);
        });

        $("input[name='demo3_21']").TouchSpin({
            initval: 40,
            buttondown_class: "btn btn-primary",
            buttonup_class: "btn btn-primary"
        });
        $("input[name='demo3_22']").TouchSpin({
            initval: 40,
            buttondown_class: "btn btn-primary",
            buttonup_class: "btn btn-primary"
        });
    },

    //Time Picker
    Components.prototype.initTimepicker = function() {
    // Time Picker
    $('#timepicker').timepicker({
        defaultTIme: false,
        icons: {
            up: 'mdi mdi-chevron-up',
            down: 'mdi mdi-chevron-down'
        }
    });
    $('#timepicker2').timepicker({
        showMeridian: false,
        icons: {
            up: 'mdi mdi-chevron-up',
            down: 'mdi mdi-chevron-down'
        }
    });
    $('#timepicker3').timepicker({
        minuteStep: 15,
        icons: {
            up: 'mdi mdi-chevron-up',
            down: 'mdi mdi-chevron-down'
        }
    });
    },

    //colorpicker
    Components.prototype.initColorpicker= function() {
        // colorpicker
        $('#default-colorpicker').colorpicker({
            format: 'hex'
        });
        $('#rgba-colorpicker').colorpicker();
        $('#component-colorpicker').colorpicker({
            format: null
        });
    },
    


    //initilizing
    Components.prototype.init = function() {
        var $this = this;
        this.initSwitchery(),
        this.initMultiSelect(),
        this.initSelect2(),
        this.initInputmask(),
        this.initTouchspin(),
        this.initTimepicker(),
        this.initColorpicker()
    },

    $.Components = new Components, $.Components.Constructor = Components

}(window.jQuery),
    //initializing main application module
function($) {
    "use strict";
    $.Components.init();
}(window.jQuery);

