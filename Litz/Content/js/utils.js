'use strict';

(function (global) {

    var app = global.AppLitz || (global.AppLitz = {});

    app.Utils = function () {
        var months = [
            'January',
            'February',
            'March',
            'April',
            'May',
            'June',
            'July',
            'August',
            'September',
            'October',
            'November',
            'December'
        ];

        var colors = {
            red: 'rgb(255, 99, 132)',
            orange: 'rgb(255, 159, 64)',
            yellow: 'rgb(255, 205, 86)',
            green: 'rgb(75, 192, 192)',
            blue: 'rgb(54, 162, 235)',
            purple: 'rgb(153, 102, 255)',
            grey: 'rgb(201, 203, 207)',
            aqua: 'rgb(2%, 73%, 67%)'
        };

        var showAlert = function (shortMsg, msg, type, $container) {
            var classType = 'info';
            if (type === 'success') {
                classType = 'success';
            }
            else if (type === 'fail') {
                classType = 'danger';
            }
            else if (type === 'info') {
                classType = 'info';
            }

            var html = '<div class="alert alert-' + classType + '"> <strong>' + shortMsg + ': </strong>' + msg + '</div>';

            $container.html(html);
        };

        var showSpinner = function() {
            $('.spinner-backdrop').fadeIn();
        };

        var hideSpinner = function () {
            $('.spinner-backdrop').fadeOut();
        };

        var getParameterByName = function(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }

        var getStartOfThisMonth = function () {
            var currentDate = new Date();

            var startOfMonth = new Date(currentDate.getFullYear(), currentDate.getMonth(), 1);

            var year = startOfMonth.getFullYear();
            var month = startOfMonth.getMonth() + 1;
            var day = startOfMonth.getDate();

            if (month < 10) {
                month = '0' + month;
            }
            if (day < 10) {
                day = '0' + day;
            }

            return year + '-' + month + '-' + day;
        };

        var getEndOfThisMonth = function () {
            var currentDate = new Date();

            var endOfMonthTillNow = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate());

            var year = endOfMonthTillNow.getFullYear();
            var month = endOfMonthTillNow.getMonth() + 1;
            var day = endOfMonthTillNow.getDate();

            if (month < 10) {
                month = '0' + month;
            }
            if (day < 10) {
                day = '0' + day;
            }

            return year + '-' + month + '-' + day;
        };

        var getStartDayOfYear = function() {
            var currentDate = new Date();

            var startOfyear = new Date(currentDate.getFullYear(), 0, 1);

            var year = startOfyear.getFullYear();
            var month = startOfyear.getMonth() + 1;
            var day = startOfyear.getDate();

            if (month < 10) {
                month = '0' + month;
            }
            if (day < 10) {
                day = '0' + day;
            } 

            return year + '-' + month + '-' + day;
        };

        var getEndDayOfYear = function () {
            var currentDate = new Date();

            // refers to current year + december 31
            // It will work as long as December continue having 31 days :P
            var endOfyear = new Date(currentDate.getFullYear(), 11, 31);

            var year = endOfyear.getFullYear();
            var month = endOfyear.getMonth() + 1;
            var day = endOfyear.getDate();

            if (month < 10) {
                month = '0' + month;
            }
            if (day < 10) {
                day = '0' + day;
            } 

            return year + '-' + month + '-' + day;
        };

        return {
            months: months,
            colors: colors,
            showAlert: showAlert,
            showSpinner: showSpinner,
            hideSpinner: hideSpinner,
            getParameterByName: getParameterByName,
            getStartOfThisMonth: getStartOfThisMonth,
            getEndOfThisMonth: getEndOfThisMonth,
            getStartDayOfYear: getStartDayOfYear,
            getEndDayOfYear: getEndDayOfYear 
        }
    };

}(window));