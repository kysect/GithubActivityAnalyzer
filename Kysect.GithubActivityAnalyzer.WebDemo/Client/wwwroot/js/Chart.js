function GenerateBarChart(teamInfo) {
   AmCharts.makeChart("chartdiv", {
        "type": "serial",
        "theme": "none",
        "categoryField": "username",
        "rotate": true,
        "startDuration": 1,
        "categoryAxis": {
            "gridPosition": "start",
            "position": "left"
        },
        "trendLines": [],
        "graphs": [
            {
                "balloonText": "totalContributions:[[value]]",
                "fillAlphas": 0.8,
                "id": "AmGraph-1",
                "lineAlpha": 0.2,
                "title": "totalContributions",
                "type": "column",
                "valueField": "totalContributions"
            },
        ],
        "guides": [],
        "valueAxes": [
            {
                "id": "ValueAxis-1",
                "position": "top",
                "axisAlpha": 0
            }
        ],
        "allLabels": [],
        "balloon": {},
        "titles": [],
        "dataProvider": teamInfo,
        "export": {
            "enabled": true
        },
        "hideCredits": true

    });
}
