'use strict';

var gulp = require('gulp'),
    gulpProtractorAngular = require('gulp-angular-protractor');

console.log('**** Started running e2e tests ****');

// Setting up the test task
gulp.task('protractor', function (callback) {
    gulp
        .src([])
        .pipe(gulpProtractorAngular({
            'configFile': 'protractor.conf.js',
            'debug': false,
            'autoStartStopServer': true
        }))
        .on('error', function (e) {
            console.log(e);
            console.log('**** Some of your e2e tests failed ****');
        })
        .on('end', function (e) {
            console.log('**** Running e2e tests finished ****');
        });
});

gulp.start('protractor');