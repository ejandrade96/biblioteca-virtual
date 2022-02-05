type = ['primary', 'info', 'success', 'warning', 'danger'];

demo = {
  initPickColor: function() {
    $('.pick-class-label').click(function() {
      var new_class = $(this).attr('new-class');
      var old_class = $('#display-buttons').attr('data-class');
      var display_div = $('#display-buttons');
      if (display_div.length) {
        var display_buttons = display_div.find('.btn');
        display_buttons.removeClass(old_class);
        display_buttons.addClass(new_class);
        display_div.attr('data-class', new_class);
      }
    });
  },

  initDocChart: function() {
    chartColor = "#FFFFFF";

    // General configuration for the charts with Line gradientStroke
    gradientChartOptionsConfiguration = {
      maintainAspectRatio: false,
      legend: {
        display: false
      },
      tooltips: {
        bodySpacing: 4,
        mode: "nearest",
        intersect: 0,
        position: "nearest",
        xPadding: 10,
        yPadding: 10,
        caretPadding: 10
      },
      responsive: true,
      scales: {
        yAxes: [{
          display: 0,
          gridLines: 0,
          ticks: {
            display: false
          },
          gridLines: {
            zeroLineColor: "transparent",
            drawTicks: false,
            display: false,
            drawBorder: false
          }
        }],
        xAxes: [{
          display: 0,
          gridLines: 0,
          ticks: {
            display: false
          },
          gridLines: {
            zeroLineColor: "transparent",
            drawTicks: false,
            display: false,
            drawBorder: false
          }
        }]
      },
      layout: {
        padding: {
          left: 0,
          right: 0,
          top: 15,
          bottom: 15
        }
      }
    };
  },

  initDashboardPageCharts: function() {

    gradientChartOptionsConfigurationWithTooltipBlue = {
      maintainAspectRatio: false,
      legend: {
        display: false
      },
      tooltips: {
        backgroundColor: '#f5f5f5',
        titleFontColor: '#333',
        bodyFontColor: '#666',
        bodySpacing: 4,
        xPadding: 12,
        mode: "nearest",
        intersect: 0,
        position: "nearest"
      },
      responsive: true,
      scales: {
        yAxes: [{
          barPercentage: 1.6,
          gridLines: {
            drawBorder: false,
            color: 'rgba(29,140,248,0.0)',
            zeroLineColor: "transparent",
          },
          ticks: {
            suggestedMin: 60,
            suggestedMax: 125,
            padding: 20,
            fontColor: "#2d2929"
          }
        }],

        xAxes: [{
          barPercentage: 1.6,
          gridLines: {
            drawBorder: false,
            color: 'rgba(225,78,202,0.1)',
            zeroLineColor: "transparent",
          },
          ticks: {
            padding: 15,
            fontColor: "#2d2929"
          }
        }]
      }
    };

    gradientBarChartConfiguration = {
      maintainAspectRatio: false,
      legend: {
        display: false
      },
      tooltips: {
        backgroundColor: '#f5f5f5',
        titleFontColor: '#333',
        bodyFontColor: '#666',
        bodySpacing: 4,
        xPadding: 12,
        mode: "nearest",
        intersect: 0,
        position: "nearest"
      },
      responsive: true,
      scales: {
        yAxes: [{
          gridLines: {
            drawBorder: false,
            color: 'rgba(29,140,248,0.1)',
            zeroLineColor: "transparent",
          },
          ticks: {
            suggestedMin: 60,
            suggestedMax: 120,
            padding: 20,
            fontColor: "#2d2929"
          }
        }],
        xAxes: [{
          gridLines: {
            drawBorder: false,
            color: 'rgba(29,140,248,0.1)',
            zeroLineColor: "transparent",
          },
          ticks: {
            padding: 20,
            fontColor: "#2d2929"
          }
        }]
      }
    };


    /**** chartBig1 AREA */

    var ctx = document.getElementById("homePageGeneralChartOfTheYear").getContext('2d');
    var config = {
      type: 'line',
      data: dataConfigGeneralChart,
      options: gradientChartOptionsConfigurationWithTooltipBlue
    };
    var gradientStroke = ctx.createLinearGradient(0, 230, 0, 50);
    gradientStroke.addColorStop(1, 'rgba(29,140,248,0.2)');
    gradientStroke.addColorStop(0.4, 'rgba(29,140,248,0.0)');
    gradientStroke.addColorStop(0, 'rgba(29,140,248,0)'); //blue colors
    config.data.datasets[0].backgroundColor = gradientStroke;
    
    var myChartData = new Chart(ctx, config);
    
    $("#generalChartTabNewStudents").click(function() {
      var data = myChartData.config.data;
      data.labels = generalChartLabels;
      data.datasets[0].data = generalChartData;
      myChartData.update();
    });
    $("#generalChartTabNewBooks").click(function() {
      var data = myChartData.config.data;
      data.labels = generalChartLabels;
      data.datasets[0].data = generalChartDataTabNewBooks;
      myChartData.update();
    });
    $("#generalChartTabLoans").click(function() {
      var data = myChartData.config.data;
      data.labels = generalChartLabels;
      data.datasets[0].data = generalChartDataTabLoans;
      myChartData.update();
    });
    $("#generalChartTabLoanReturns").click(function() {
      var data = myChartData.config.data;
      data.labels = generalChartLabels;
      data.datasets[0].data = generalChartDataTabLoanReturns;
      myChartData.update();
    });


    /**** ctxNewStudents AREA */

    var ctxNewStudents = document.getElementById("newStudentsOfTheWeekChart").getContext("2d");
    var configChartNewStudents = {
      type: 'bar',
      responsive: true,
      legend: {
        display: false
      },
      data: dataConfigChartNewStudents,
      options: gradientBarChartConfiguration
    };
    var gradientStroke = ctxNewStudents.createLinearGradient(0, 230, 0, 50);
    gradientStroke.addColorStop(1, 'rgba(29,140,248,0.2)');
    gradientStroke.addColorStop(0.4, 'rgba(29,140,248,0.0)');
    gradientStroke.addColorStop(0, 'rgba(29,140,248,0)'); //blue colors
    configChartNewStudents.data.datasets[0].backgroundColor = gradientStroke;
    configChartNewStudents.data.datasets[0].hoverBackgroundColor = gradientStroke;
  
    var myChartNewStudents = new Chart(ctxNewStudents, configChartNewStudents);

    
    /**** ctxNewBooks AREA */
    
    var ctxNewBooks = document.getElementById("newBooksOfTheWeekChart").getContext("2d");
    var configChartNewBooks = {
      type: 'bar',
      responsive: true,
      legend: {
        display: false
      },
      data: dataConfigChartNewBooks,
      options: gradientBarChartConfiguration
    };
    var gradientStroke = ctxNewBooks.createLinearGradient(0, 230, 0, 50);
    gradientStroke.addColorStop(1, 'rgba(29,140,248,0.2)');
    gradientStroke.addColorStop(0.4, 'rgba(29,140,248,0.0)');
    gradientStroke.addColorStop(0, 'rgba(29,140,248,0)'); //blue colors
    configChartNewBooks.data.datasets[0].backgroundColor = gradientStroke;
    configChartNewBooks.data.datasets[0].hoverBackgroundColor = gradientStroke;

    var myChartNewBooks = new Chart(ctxNewBooks, configChartNewBooks);

    
    /**** ctxLoans AREA */

    var ctxLoans = document.getElementById("loansOfTheWeekChart").getContext("2d");
    var configChartLoans = {
      type: 'bar',
      responsive: true,
      legend: {
        display: false
      },
      data: dataConfigChartLoans,
      options: gradientBarChartConfiguration
    };
    var gradientStroke = ctxLoans.createLinearGradient(0, 230, 0, 50);
    gradientStroke.addColorStop(1, 'rgba(29,140,248,0.2)');
    gradientStroke.addColorStop(0.4, 'rgba(29,140,248,0.0)');
    gradientStroke.addColorStop(0, 'rgba(29,140,248,0)'); //blue colors
    configChartLoans.data.datasets[0].backgroundColor = gradientStroke;
    configChartLoans.data.datasets[0].hoverBackgroundColor = gradientStroke;

    var myChartLoans = new Chart(ctxLoans, configChartLoans);


    /**** ctxLoanReturns AREA */

    var ctxLoanReturns = document.getElementById("loanReturnsTheWeekChart").getContext("2d");
    var configChartLoanReturns = {
      type: 'bar',
      responsive: true,
      legend: {
        display: false
      },
      data: dataConfigChartLoanReturns,
      options: gradientBarChartConfiguration
    }
    var gradientStroke = ctxLoanReturns.createLinearGradient(0, 230, 0, 50);
    gradientStroke.addColorStop(1, 'rgba(29,140,248,0.2)');
    gradientStroke.addColorStop(0.4, 'rgba(29,140,248,0.0)');
    gradientStroke.addColorStop(0, 'rgba(29,140,248,0)'); //blue colors
    configChartLoanReturns.data.datasets[0].backgroundColor = gradientStroke;
    configChartLoanReturns.data.datasets[0].hoverBackgroundColor = gradientStroke;

    var myChartLoanReturns = new Chart(ctxLoanReturns, configChartLoanReturns);
  },
};