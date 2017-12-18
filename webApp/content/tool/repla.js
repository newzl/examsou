function replaceGif(str) {
    $.each([
    { o: /\[0.gif\]/gm, n: '<sub>0</sub>' },
    { o: /\[1.gif\]/gm, n: '<sub>1</sub>' },
    { o: /\[2.gif\]/gm, n: '<sub>2</sub>' },
    { o: /\[~2.gif\]/gm, n: '<sup>2</sup>' },
    { o: /\[~2\+.gif\]/gm, n: '<sup>2+</sup>' },
    { o: /\[3.gif\]/gm, n: '<sub>3</sub>' },
    { o: /\[~3\+.gif\]/gm, n: '<sup>3+</sup>' },
    { o: /\[4.gif\]/gm, n: '<sub>4</sub>' },
    { o: /\[5.gif\]/gm, n: '<sub>5</sub>' },
    { o: /\[6.gif\]/gm, n: '<sub>6</sub>' },
    { o: /\[7.gif\]/gm, n: '<sub>7</sub>' },
    { o: /\[8.gif\]/gm, n: '<sub>8</sub>' },
    { o: /\[9.gif\]/gm, n: '<sub>9</sub>' },
    { o: /\[~9.gif\]/gm, n: '<sup>9</sup>' },
    { o: /\[~10.gif\]/gm, n: '<sup>10</sup>' },
    { o: /\[~11.gif\]/gm, n: '<sup>11</sup>' },
    { o: /\[12.gif\]/gm, n: '<sub>12</sub>' },
    { o: /\[~12.gif\]/gm, n: '<sup>12</sup>' },
    { o: /\[~a.gif\]/gmi, n: '<sup>a</sup>' },
    { o: /\[~b.gif\]/gmi, n: '<sup>b</sup>' },
    { o: /\[e.gif\]/gmi, n: '<sub>E</sub>' },
    { o: /\[h.gif\]/gmi, n: '<sub>H</sub>' },
    { o: /\[i.gif\]/gmi, n: '<sub>i</sub>' },
    { o: /\[j.gif\]/gmi, n: '<sub>j</sub>' },
    { o: /\[m.gif\]/gmi, n: '<sub>m</sub>' },
    { o: /\[n.gif\]/gmi, n: '<sub>n</sub>' },
    { o: /\[~n.gif\]/gmi, n: '<sup>n</sup>' },
    { o: /\[~t.gif\]/gmi, n: '<sup>t</sup>' },
    { o: /\[~u.gif\]/gmi, n: '<sup>u</sup>' },
    { o: /\[~w.gif\]/gmi, n: '<sup>w</sup>' },
    { o: /\[p.gif\]/gmi, n: '<sub>p</sub>' },
    { o: /\[x.gif\]/gmi, n: '<sub>x</sub>' },
    { o: /\[~x.gif\]/gmi, n: '<sup>x</sup>' },

    { o: /\[~\+.gif\]/gm, n: '<sup>+</sup>' },
    { o: /\[~\-.gif\]/gm, n: '<sup>-</sup>' }
    ],
    function (i, ri) {
        str = str.replace(ri.o, ri.n);
    });
    return str;
}