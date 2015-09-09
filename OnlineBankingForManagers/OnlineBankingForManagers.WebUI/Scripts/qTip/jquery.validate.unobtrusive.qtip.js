$(document).ready(function () {   

    $('[title]').filter(':not(.input-validation-error)').each(function () {

        var toLeft = $(this).offset().left > $(document).width() / 2;

        $(this).filter(':not(select)').filter(':not(input),[type="submit"]').qtip({
            override: false,
            position: {
                my: toLeft ? 'right bottom' : 'left bottom',
                at: toLeft ? 'left center' : 'right center',
                target: 'mouse',
                adjust: {
                    mouse: true,
                    x: toLeft ? -20 : 20,
                    y: -10
                }
            },
            style: {
            classes: 'qtip-light qtip-shadow'
            },
            show: {
                effect: function() {
                    $(this).slideDown();
                }
            },
            hide: {
                effect: function() {
                    $(this).slideUp();
                   
                }
            }
        });

        $(this).filter("input,select").filter(":not([type='submit'])").filter(':not(.input-validation-error)').qtip({
            override: false,
            content: $(this).title,
            position: {
                my: 'center left', 
                at: 'center right'  
            },
            show: {
                event: 'focus mouseenter',
                effect: function () {
                    $(this).fadeTo(500, 1);
                }                
            },
            hide: {
                event: 'blur mouseleave',
                effect: function () {
                    $(this).hide('puff', 500);
                }
            },
            style: {
                 classes: 'qtip-blue qtip-rounded qtip-shadow'
            }
        });        
    });

    $('.focus').focus();

    $('input').filter('.input-validation-error').each(function () {
        $(this).focus();
        var cont = $('span').filter('[data-valmsg-for="' + $(this).attr('id') + '"]');
        $(this).qtip({
            override: false,
            content: cont,
            position: {
                my: 'center left',
                at: 'center right'
            },
            show: {
                ready: true,
                event: 'focus mouseenter',
                effect: function () {
                    $(this).fadeTo(500, 1);
                }

            },
            hide: {
                event: false,
                effect: function () {
                    $(this).hide('puff', 500);
                }
            },
            style: {
                classes: 'qtip-red qtip-rounded qtip-shadow'
            }
        });
    });

    $('form').each(function () {
        OverrideUnobtrusiveSettings(this);
    });
    //in case someone calls $.validator.unobtrusive.parse, override it also
    var oldUnobtrusiveParse = $.validator.unobtrusive.parse;
    $.validator.unobtrusive.parse = function (selector) {
        oldUnobtrusiveParse(selector);
        $('form').each(function () {
            OverrideUnobtrusiveSettings(this);
        });
    };
    //replace validation settings function
    function OverrideUnobtrusiveSettings(formElement) {
        var settngs = $.data(formElement, 'validator').settings;
        //standard qTip2 stuff copied from sample
        settngs.errorPlacement = function (error, element) {
            // Set positioning based on the elements position in the form
            var elem = $(element);
       //     debugger;
            // Check we have a valid error message
            if (!error.is(':empty')) {
                elem.qtip('option', 'show.ready', true);
                var _er = $(error)[0].innerText;
                // Apply the tooltip only if it isn't valid
                elem.qtip('option', 'content.text', _er);               
                elem.qtip('option', 'hide.event', false);
                elem.qtip('option', 'style.classes', 'qtip-red qtip-rounded qtip-shadow');
            }
                // If the error is empty, remove the qTip
            else {
                var _tx = elem.attr('oldtitle');
                elem.qtip('option', 'content.text', _tx);
                elem.qtip('option', 'hide.event', 'blur mouseleave');
                elem.qtip('option', 'show.event', 'focus mouseenter');
                elem.qtip('option', 'style.classes', 'qtip-blue qtip-rounded qtip-shadow');
                elem.qtip('option', 'show.ready', false);
            }
        };
        settngs.success = $.noop;
    }

    

});