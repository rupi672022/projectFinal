/**
* Theme:  Moltran - Responsive Bootstrap 4 Admin & Dashboard
* Author: Coderthemes
* File:   widgets
*/

$( document ).ready(function() {

  var DrawSparkline = function() {
      $('#sparkline1').sparkline([0, 23, 43, 35, 44, 45, 56, 37, 40], {
          type: 'line',
          width: "100%",
          height: '165',
          chartRangeMax: 50,
          lineColor: '#ddd',
          fillColor: 'rgba(221, 221, 221, 0.3)',
          highlightLineColor: 'rgba(0,0,0,.1)',
          highlightSpotColor: 'rgba(0,0,0,.2)',
      });

      $('#sparkline1').sparkline([25, 23, 26, 24, 25, 32, 30, 24, 19], {
          type: 'line',
          width: "100%",
          height: '165',
          chartRangeMax: 40,
          lineColor: '#86C3F4',
          fillColor: 'rgba(134, 195, 244, 0.3)',
          composite: true,
          highlightLineColor: 'rgba(0,0,0,.1)',
          highlightSpotColor: 'rgba(0,0,0,.2)',
      });

      $('#sparkline2').sparkline([3, 6, 7, 8, 6, 4, 7, 10, 12, 7, 4, 9, 12, 13, 11, 12], {
          type: 'bar',
          height: '165',
          barWidth: '10',
          barSpacing: '3',
          barColor: '#86C3F4'
      });

      $('#sparkline3').sparkline([20, 40, 30, 10], {
          type: 'pie',
          width: '165',
          height: '165',
          sliceColors: ['#64b5f6', '#90caf9', "#bbdefb", "#42a5f5"]
      });

      $('#sparkline4').sparkline([0, 23, 43, 35, 44, 45, 56, 37, 40], {
          type: 'line',
          width: "100%",
          height: '165',
          chartRangeMax: 50,
          lineColor: '#86C3F4',
          fillColor: 'transparent',
          highlightLineColor: 'rgba(0,0,0,.1)',
          highlightSpotColor: 'rgba(0,0,0,.2)'
      });

      $('#sparkline4').sparkline([25, 23, 26, 24, 25, 32, 30, 24, 19], {
          type: 'line',
          width: "100%",
          height: '165',
          chartRangeMax: 40,
          lineColor: '#ddd',
          fillColor: 'transparent',
          composite: true,
          highlightLineColor: 'rgba(0,0,0, .1)',
          highlightSpotColor: 'rgba(0,0,0, .2)'
      });

      $('#sparkline6').sparkline([3, 6, 7, 8, 6, 4, 7, 10, 12, 7, 4, 9, 12, 13, 11, 12], {
          type: 'bar',
          height: '165',
          barWidth: '10',
          barSpacing: '3',
          barColor: '#dddddd'
      });

      $('#sparkline6').sparkline([3, 6, 7, 8, 6, 4, 7, 10, 12, 7, 4, 9, 12, 13, 11, 12], {
          type: 'line',
          width: "100%",
          height: '165',
          lineColor: '#86C3F4',
          fillColor: 'transparent',
          composite: true,
          highlightLineColor: 'rgba(0,0,0,.1)',
          highlightSpotColor: 'rgba(0,0,0,.2)'
      });


  },
      DrawMouseSpeed = function () {
          var mrefreshinterval = 500; // update display every 500ms
          var lastmousex=-1;
          var lastmousey=-1;
          var lastmousetime;
          var mousetravel = 0;
          var mpoints = [];
          var mpoints_max = 30;
          $('html').mousemove(function(e) {
              var mousex = e.pageX;
              var mousey = e.pageY;
              if (lastmousex > -1) {
                  mousetravel += Math.max( Math.abs(mousex-lastmousex), Math.abs(mousey-lastmousey) );
              }
              lastmousex = mousex;
              lastmousey = mousey;
          });
          var mdraw = function() {
              var md = new Date();
              var timenow = md.getTime();
              if (lastmousetime && lastmousetime!=timenow) {
                  var pps = Math.round(mousetravel / (timenow - lastmousetime) * 1000);
                  mpoints.push(pps);
                  if (mpoints.length > mpoints_max)
                      mpoints.splice(0,1);
                  mousetravel = 0;
                  $('#sparkline5').sparkline(mpoints, {
                      tooltipSuffix: ' pixels per second',
                      type: 'line',
                      width: "100%",
                      height: '165',
                      chartRangeMax: 50,
                      lineColor: '#86C3F4',
                      fillColor: 'rgba(134, 195, 244, 0.3)',
                      highlightLineColor: 'rgba(254, 98, 11, .1)',
                      highlightSpotColor: 'rgba(254, 98, 11, .2)',
                  });
              }
              lastmousetime = timenow;
              setTimeout(mdraw, mrefreshinterval);
          }
          // We could use setInterval instead, but I prefer to do it this way
          setTimeout(mdraw, mrefreshinterval); 
      };
  
  DrawSparkline();
  DrawMouseSpeed();
  
  var resizeChart;

  $(window).resize(function(e) {
      clearTimeout(resizeChart);
      resizeChart = setTimeout(function() {
          DrawSparkline();
          DrawMouseSpeed();
      }, 300);
  });
});