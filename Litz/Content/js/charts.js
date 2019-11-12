'use strict';

(function (global, app, $) {
    var utils = new app.Utils();

    app.Charts = function () {

        var plotAccountSummaryPerGroupPieChart = function ($container, periodStart, periodEnd, title) {
            var params = {
                periodStart: periodStart,
                periodEnd: periodEnd
            };

            $.getJSON('/Home/GetSummaryPerGroup', params, function (dataJson) {

                var data = [];

                for (var i = 0; i < dataJson.length; i++) {
                    data.push({
                        name: dataJson[i].Name,
                        color: dataJson[i].Color,
                        amount: dataJson[i].Legend,
                        y: dataJson[i].Yaxis
                    });
                }

                $container.highcharts({
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        type: 'pie',
                        style: {
                            fontFamily: 'Raleway, sans-serif'
                        }
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: title,
                        align: 'center'
                    },
                    tooltip: {
                        borderWidth: 0,
                        backgroundColor: '#f6f6f6',
                        style: {
                            "fontSize": "16px"
                        },
                        headerFormat: '<span>{point.key}</span><br/>',
                        pointFormat: '<b class="bold text-uppercase" "text-center">R$ {point.amount}</b>'
                    },
                    legend: {
                        align: 'left',
                        verticalAlign: 'top',
                        layout: 'vertical'
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: true,
                                format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                style: {
                                    color: (Highcharts.theme && Highcharts.theme.contrastTextColor) ||
                                        'black'
                                }
                            },
                            showInLegend: true
                        }
                    },
                    series: [
                        {
                            name: 'Quantidade',
                            colorByPoint: true,
                            data: data,
                            innerSize: '60%'
                        }
                    ]
                });
            });
        };

        return {
            plotAccountSummaryPerGroupPieChart: plotAccountSummaryPerGroupPieChart
        }
    };

}(window, AppLitz, jQuery));