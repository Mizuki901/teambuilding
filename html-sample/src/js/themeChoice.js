$(function () {
  // 新規追加ボタン
  $('.buttonNewPaper').on('click', function () {
    $('.dialogNewPaper').modal()
  })

  // ダイアログ表示前にJavaScriptで操作する
  $('.dialogNewPaper').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget)
    var recipient = button.data('whatever')
    var modal = $(this)
    modal.find('.modal-body .recipient').text(recipient)
  // modal.find('.modal-body input').val(recipient)
  })

  // ダイアログ表示直後にフォーカスを設定する
  $('.dialogNewPaper').on('shown.bs.modal', function (event) {
    $(this).find('.modal-footer .btn-default').focus()
  })

  $('.dialogNewPaper').on('click', '.modal-footer .btn-primary', function () {
    $('.dialogNewPaper').modal('hide')
    alert('テーマを追加しました')
  })
})
