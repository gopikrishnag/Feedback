/// <binding AfterBuild='compile-sass, compress-js' />
 
var gulp = require('gulp');
var sass = require('gulp-sass');
var uglify = require('gulp-uglify');
var pipeline = require('readable-stream').pipeline;

gulp.task('compile-sass', function () {
    return gulp.src("node_modules/govuk-frontend/govuk/all.scss")
        .pipe(sass({ outputStyle: "compressed" }))
        .pipe(gulp.dest('wwwroot/css'));
});

gulp.task('compress-js', function () {
    return pipeline(
        gulp.src(["node_modules/govuk-frontend/govuk/all.js", "node_modules/govuk-frontend/govuk/common.js"]),
        uglify(),
        gulp.dest('wwwroot/js')
    );
});