$(function () {
  // 新規追加ボタン
  $('.buttonNewPaper').on('click', function () {
    $('.dialogNewPaper').modal()
    return false
  })

  // ログアウトボタン
  $('.buttonLogout').on('click', function () {
    alert('ログアウトしました')
    return false
  })

  // 一か所のみ参加を付けるように制御
  $('.toggle-join').on('change', function(event) {
    if (event.currentTarget.checked) {
        // console.log('checked')
        $('input', $('.toggle-join>.toggle').not('.off')).prop('checked', false).change()
    }
    return false
  })

  // [新規追加ダイアログ] 表示前操作
  $('.dialogNewPaper').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget)
    var recipient = button.data('whatever')
    var modal = $(this)
    modal.find('.modal-body .recipient').text(recipient)
  })

  // [新規追加ダイアログ] 表示直後操作
  $('.dialogNewPaper').on('shown.bs.modal', function (event) {
    // フォーカス設定
    $(this).find('.modal-footer .btn-default').focus()
  })

  // [新規追加ダイアログ] 追加ボタン
  $('.themeAddButton', '.dialogNewPaper').on('click', function () {
    $('.dialogNewPaper').modal('hide')
    alert('テーマを追加しました')
  })

  // [新規追加ダイアログ] 色変更
  $('li.paperColorSelect').on('click', function(event) {
    var newColor = $(event.currentTarget).attr('value')
    var dialogContents = $('div.modal-content', 'div.dialogNewPaper')

    console.log($(event.currentTarget).attr('value'))
    dialogContents.removeClass('theme-paper-' + dialogContents.attr('value'))
    dialogContents.attr('value', newColor)
    dialogContents.addClass('theme-paper-' + newColor)
  })

})
