/**
* Theme:  Moltran - Responsive Bootstrap 4 Admin & Dashboard
* Author: Coderthemes
* File:   chartjs
*/

!function ($) {
    "use strict";

    var ChartJs = function () { };

    ChartJs.prototype.respChart = function respChart(selector, type, data, options) {
        Chart.defaults.global.defaultFontColor="#6c7897",
        Chart.defaults.scale.gridLines.color="rgba(108, 120, 151, 0.1)";
        // get selector by context
        var ctx = selector.get(0).getContext("2d");
        // pointing parent container to make chart js inherit its width
        var container = $(selector).parent();

        // enable resizing matter
        $(window).resize(generateChart);

        // this function produce the responsive Chart JS
        function generateChart() {
            // make chart width fit with its container
            var ww = selector.attr('width', $(container).width());
            switch (type) {
                case 'Line':
                    new Chart(ctx, { type: 'line', data: data, options: options });
                    break;
                case 'Doughnut':
                    new Chart(ctx, { type: 'doughnut', data: data, options: options });
                    break;
                case 'Pie':
                    new Chart(ctx, { type: 'pie', data: data, options: options });
                    break;
                case 'Bar':
                    new Chart(ctx, { type: 'bar', data: data, options: options });
                    break;
                case 'Radar':
                    new Chart(ctx, { type: 'radar', data: data, options: options });
                    break;
                case 'PolarArea':
                    new Chart(ctx, { type: 'polarArea', data: data, options: options });
                    break;
            }
            // Initiate new chart or Redraw

        };
        // run function - render chart at first load
        generateChart();
    },
        //init
        ChartJs.prototype.init = function () {
            //creating lineChart
            var data = {
                labels: ["January", "February", "March", "April", "May", "June", "July"],
                datasets: [
                    {
                        backgroundColor: "rgba(49, 126, 235, 0.75)",
                        borderColor: "rgba(49, 126, 235, 0.75)",
                        pointColor : "#fff",
                        pointStrokeColor : "rgba(49, 126, 235, 0.75)",
                        data: [33, 52, 63, 92, 50, 53, 46]
                    },

                    {
                        backgroundColor: "#dcdcdc",
                        borderColor: "#dcdcdc",
                        pointColor: "#fff",
                        pointStrokeColor: "#dcdcdc",
                        data: [15, 25, 40, 35, 32, 9, 33]
                    }

                ]
            }

            var option = {
                legend: {
                    display: false,
                }
            };

            this.respChart($("#lineChart"), 'Line', data,option);

            //donut chart
            var data1 = {
                labels: ["Jan", "Feb", "Mar", "Apr"],
                datasets: [
                    {
                        
                        data: [80, 50, 80, 50],
                        backgroundColor: [
                            "#60b1cc",
                            "#bac3d2",
                            "#4697ce",
                            "#6c85bd"
                        ]
                    }]
            }
   
            this.respChart($("#doughnut"), 'Doughnut', data1);


            //Pie chart
            var data2 = {
                labels: ["Series A", "Series B", "Series C"],
                datasets: [
                    {
                        data: [40, 80, 70],
                        backgroundColor: [
                            "#dcdcdc",
                            "#317eeb",
                            "#999999"
                        ]
                    }]
            }
            this.respChart($("#pie"), 'Pie', data2);


            //barchart
            var data3 = {
                labels: ["January", "February", "March", "April", "May", "June", "July"],
                datasets: [
                    {
                        backgroundColor: "#317eeb",
                        borderColor: "#317eeb",
                        data: [65, 59, 90, 81, 56, 55, 40]
                    },
                    {
                        backgroundColor: "#dcdcdc",
                        borderColor: "#dcdcdc",
                        data: [28, 48, 40, 19, 96, 27, 100]
                    }
                ]
            }

            var option = {
                legend: {
                    display: false,
                }
            };
            this.respChart($("#bar"), 'Bar', data3, option);

            //radar chart
            var data4 = {
                labels: ["Eating", "Drinking", "Sleeping", "Designing", "Coding", "Partying", "Running"],
                datasets: [
                    {   label: "Desktops",
                        backgroundColor: "rgba(49, 126, 235, 0.5)",
                        borderColor: "rgba(49, 126, 235, 0.75)",
                        pointBackgroundColor: "rgba(49, 126, 235, 1)",
                        pointBorderColor: "#fff",
                        pointHoverBackgroundColor: "#fff",
                        data: [65, 59, 90, 81, 56, 55, 40]
                    },
                    {   label: "Tablets",
                        backgroundColor: "rgba(220, 220, 220, 0.5)",
                        borderColor: "rgba(220, 220, 220, 0.75)",
                        pointBackgroundColor: "rgba(220, 220, 220,1)",
                        pointBorderColor: "#fff",
                        pointHoverBackgroundColor: "#fff",
                        pointHoverBorderColor: "rgba(255,99,132,1)",
                        data: [28, 48, 40, 19, 96, 27, 100]
                    }

                ]
            }
            this.respChart($("#radar"), 'Radar', data4);

            var data5 = {
                datasets: [{
                    data: [
                        30,
                        90,
                        24,
                        58,
                        82,
                        8
                    ],
                    backgroundColor: [
                        "#60b1cc",
                        "#bac3d2",
                        "#4697ce",
                        "#6c85bd",
                        "#317eeb",
                        "#1ca8dd"
                    ],
                }],
                labels: [
                    "Series 1",
                    "Series 2",
                    "Series 3",
                    "Series 4",
                    "Series 5",
                    "Series 6",
                ]
               
            };
            this.respChart($("#polarArea"), 'PolarArea', data5);
        },
        $.ChartJs = new ChartJs, $.ChartJs.Constructor = ChartJs

}(window.jQuery),

    //initializing 
    function ($) {
        "use strict";
        $.ChartJs.init()
    }(window.jQuery);