/**
    @Name：layui.uploadpic 上传图片模块
    @Author：hllive
 */
layui.define(['upload', 'form'], function (exports) {
    "use strict";
    var upload = layui.upload, form = layui.form;
    exports('uploadpic', {
        //options:elem,hw(高宽),size,oldpic,required(是否必选)
        init: function (options) {
            form.verify({
                uppic: function (v, i) {
                    if (v.length <= 0) return '图片文件不能为空，请上传图片';
                }
            });
            var dom = $(options.elem),
                _hw = options.hw || '150x150',
                _size = options.size || 30,
                _requ = options.required || true,
                _url = '/handler/root/upload_image.ashx',

                drag = $('<div/>', { 'class': 'layui-upload-drag' }),
                p = $('<p/>').css({ 'margin-top': '5px' }),
                btn = $('<a/>', { 'class': 'layui-btn layui-btn-radius layui-btn-danger' }).text('立即上传').hide(),
                input;

            if (_requ) input = $('<input type="text" name="picurl" required lay-verify="uppic" class="layui-input" style="width:270px" readonly>').hide();
            else input = $('<input type="text" name="picurl" class="layui-input" style="width:270px" readonly>').hide();

            if (options.oldpic !== undefined && options.oldpic !== null) {
                drag.append($('<img/>', { src: options.oldpic }));
                _url += '?oldpic=' + options.oldpic;
                input.val(options.oldpic).show();
            }
            else {
                drag.append('<i class="layui-icon">&#xe67c;</i><p>点击或将文件拖拽到此处</p><p>支持jpeg、png、gif</p><p>不能大于' + _size + 'KB，尺寸' + _hw + '像素</p>');
            }
            dom.empty();
            dom.append(drag);
            p.append(btn);
            p.append(input)
            dom.append(p);

            upload.render({
                elem: drag,
                url: _url, auto: false,
                accept: 'images', exts: 'jpg|jpeg|png|gif', size: _size,
                bindAction: btn,
                choose: function (obj) {
                    obj.preview(function (i, f, result) {
                        btn.show();
                        input.hide();
                        drag.empty().append(
                            $('<img/>', { src: result })
                        );
                    });
                },
                before: function () { layer.load(2); },
                done: function (res) {
                    switch (res.state) {
                        case 1:
                            layer.msg('上传成功', { icon: 1 });
                            btn.hide();
                            input.val(res.msg).show();
                            break;
                        case 0:
                            layer.msg(res.msg, { icon: 2 });
                            break;
                        default:
                            alert('error');
                            console.log(res);
                            break;
                    }
                    layer.closeAll('loading');
                }
            });
        }
    });
});