// gulp
var gulp = require('gulp');

// plugins
var wiredep = require('wiredep').stream;
var usemin = require('gulp-usemin');
var connect = require('gulp-connect');

gulp.task('wiredep', function () {
  gulp.src('./index.html')
    .pipe(wiredep())
  .pipe(gulp.dest('./'));
});

gulp.task('watch', function () {	
  gulp.watch(['./css/**/*.css'], ['htmlReload']);
  gulp.watch(['./**/*.html'], ['htmlReload']);
});

gulp.task('htmlReload', function () {
  gulp.src('./**/*.html')
    .pipe(connect.reload());
});

gulp.task('connect', function() {
  connect.server({
    livereload: true
  });
});

gulp.task('develop',['connect', 'watch']);

gulp.task('default', ['develop']);