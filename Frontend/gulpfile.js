const { src, dest, watch } = require("gulp");
const sass = require("gulp-sass")(require("sass"));
const postcss = require("gulp-postcss");
const autoprefixer = require("autoprefixer");

function css() {
  return src("./sass/*.scss") // Učitavanje SCSS fajlova
    .pipe(sass().on("error", sass.logError)) // Kompajliranje SCSS u CSS
    .pipe(postcss([autoprefixer()])) // Dodavanje prefiksa za pretraživače
    .pipe(dest("./css/")); // Spremanje kompiliranog CSS-a
}

exports.buildCss = css;

exports.watch = function () {
  watch("./sass/*.scss", css); // Posmatranje promena u SCSS fajlovima
};
