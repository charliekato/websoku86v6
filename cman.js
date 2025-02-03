
 //===============================================================
 //  �t�B���^�e�[�u���̋��ʕϐ��@�ݒ�v�I
 //===============================================================
var gTabldID = 'sampleTable';  // �e�[�u���̃G���A��ID��ݒ�
var gTfStartRow = 0;
var gTfColList  = [];             // �{�^�����z�u����Ă����ԍ�
var gTfListSave = {};             // �t�B���^���X�g�̕ۑ����
 
 //===============================================================
 //  �I�����[�h�Ńe�[�u�������ݒ�֐���CALL
 //===============================================================
window.onload = function() {
  tFilterInit();
}

function tFilterInit(){
 //==============================================================
 //  �e�[�u���̏����ݒ�
 //==============================================================
  var wTABLE  = document.getElementById(gTabldID);
  var wTR     = wTABLE.rows;
  var wAddBtn = '';
 
  // ------------------------------------------------------------
  //   �e�[�u�������t�B���^�{�^����t����
  // ------------------------------------------------------------
  for(var i=0; i < wTR.length; i++){
 
    var wTD     = wTABLE.rows[i].cells;
 
    for(var j=0; j < wTD.length; j++){
 
      // --- �ucmanFilterBtn�v�̒�`������Z����ΏۂƂ��� ------
      if(wTD[j].getAttribute('cmanFilterBtn') !== null){
 
        // --- �t�B���^�Ώۂ̓{�^���̎��̍s���� -----------------
        gTfStartRow = i + 1;
 
        // --- �{�^����ǉ��i�摜��svg���g�p�j ------------------
        wAddBtn  = '<div class="tfArea">';
        wAddBtn += '<svg class="tfImg" id="tsBtn_'+j+'" onclick="tFilterCloseOpen('+j+')"><path d="M0 0 L9 0 L6 4 L6 8 L3 8 L3 4Z"></path></svg>';
        wAddBtn += '<div class="tfList" id="tfList_'+j+'" style="display:none">';
        wAddBtn += tFilterCreate(j);
        wAddBtn += '</div>';
        wAddBtn += '</div>';
        wTD[j].innerHTML = wTD[j].innerHTML+wAddBtn;
 
        // --- �t�B���^�{�^���Ȃ���ۑ� -----------------------
        gTfColList.push(j);
      }
    }
 
    // --- �{�^����t������ȍ~�̍s�͖������� -------------------
    if(wAddBtn != ''){
      gSortBtnRow = i;
      break;
    }
 
  }
}

function tFilterCreate(argCol){
 //==============================================================
 //  �w���̃t�B���^���X�g�쐬
 //==============================================================
 
  var wTABLE    = document.getElementById(gTabldID);
  var wTR       = wTABLE.rows;
  var wItem     = [];              // �N���b�N���ꂽ��̒l
  var wNotNum   = 0;               // 1 : �����łȂ�
  var wItemSave = {};              // �t�B���^�ɐݒ肵���l���L�[
  var rcList    = '';              // �Ԃ��t�B���^���X�g
 
  // ------------------------------------------------------------
  //  �N���b�N���ꂽ��̒l���擾����
  // ------------------------------------------------------------
  for(var i=gTfStartRow; i < wTR.length; i++){
    var j = i - gTfStartRow;
 
    wItem[j] = wTR[i].cells[argCol].innerText.toString();
 
    if(wItem[j].match(/^[-]?[0-9,\.]+$/)){
    }else{
        wNotNum = 1;
    }
 
  }
 
  // ------------------------------------------------------------
  //  ��̒l�Ń\�[�g�����s
  // ------------------------------------------------------------
    if(wNotNum == 0){
      wItem.sort(sortNumA);           // ���l�ŏ���
    }else{
      wItem.sort(sortStrA);           // �����ŏ���
    }
 
  // ------------------------------------------------------------
  //  �u���ׂāv�̃`�F�b�N�{�b�N�X�쐬
  // ------------------------------------------------------------
  var wItemId =  id='tfData_ALL_'+argCol;
 
  rcList += '<div class="tfMeisai">';
  rcList += '<input type="checkbox" id="'+wItemId+'" checked onclick="tFilterAllSet('+argCol+')">';
  rcList += '<label for="'+wItemId+'">(���ׂ�)</label>';
  rcList += '</div>';
 
  // ------------------------------------------------------------
  //  ��̒l�Ńt�B���^�̃`�F�b�N�{�b�N�X���쐬����
  //    �`�F�b�N�{�b�N�X��form�ň͂�
  // ------------------------------------------------------------
  rcList += '<form name="tfForm_'+argCol+'">';
 
  for(var i=0; i < wItem.length; i++){
 
    wVal = trim(wItem[i]);
 
    if(wVal in wItemSave){
      // ---�l�Ń`�F�b�N�{�b�N�X���쐬����Ă���(�d��) ----------
    }else{
 
      // ---�`�F�b�N�{�b�N�X�̍쐬 ------------------------------
      wItemId =  id='tfData_'+argCol+'_r'+i;
      rcList += '<div class="tfMeisai">';
      rcList += '<input type="checkbox" id="'+wItemId+'" value="'+wVal+'" checked onclick="tFilterClick('+argCol+')">';
      rcList += '<label for="'+wItemId+'">'+( wVal=='' ? '(��)' : wVal )+'</label>';
      rcList += '</div>';
 
      // ---�d������p�Ƀ`�F�b�N�{�b�N�X�̒l��ۑ� --------------
      wItemSave[wVal]='1';
    }
  }
  rcList += '</form>';
 
  // ------------------------------------------------------------
  //  �������o��input���쐬
  // ------------------------------------------------------------
  rcList += '<div class="tfInStr">';
  rcList += '<input type="text" placeholder="�܂ޕ������o" id="tfInStr_'+argCol+'">';
  rcList += '</div>';
 
  // ------------------------------------------------------------
  //  �uOK�v�uCancel�v�{�^���̍쐬
  // ------------------------------------------------------------
  rcList += '<div class="tfBtnArea">';
  rcList += '<input type="button" value="OK" onclick="tFilterGo()">';
  rcList += '<input type="button" value="Cancel" onclick="tFilterCancel('+argCol+')">';
  rcList += '</div>';
 
  // ------------------------------------------------------------
  //  �쐬����html��Ԃ�
  // ------------------------------------------------------------
  return rcList;
 
}

function tFilterClick(argCol){
 //==============================================================
 //  �t�B���^���X�g�̃`�F�b�N�{�b�N�X�N���b�N
 //    �u���ׂāv�̃`�F�b�N�{�b�N�X�Ɛ����������킹��
 //==============================================================
  var wForm   = document.forms['tfForm_'+argCol];
  var wCntOn  = 0;
  var wCntOff = 0;
  var wAll    = document.getElementById('tfData_ALL_'+argCol);   // �u���ׂāv�̃`�F�b�N�{�b�N�X
 
  // --- �e�`�F�b�N�{�b�N�X�̏�Ԃ��W�v���� ---------------------
  for (var i = 0; i < wForm.elements.length; i++){
    if(wForm.elements[i].type == 'checkbox'){
      if (wForm.elements[i].checked) { wCntOn++;  }
      else                           { wCntOff++; }
    }
  }
 
  // --- �e�`�F�b�N�{�b�N�X�W�v�Łu���ׂāv�𐮔����� -----------
  if((wCntOn == 0)||(wCntOff == 0)){
    wAll.checked = true;             // �u���ׂāv���`�F�b�N����
    tFilterAllSet(argCol);           // �e�t�B���^�̃`�F�b�N����
  }else{
     wAll.checked = false;           // �u���ׂāv���`�F�b�N���O��
  }
}

function tFilterCancel(argCol){
 //==============================================================
 //  �L�����Z���{�^������
 //==============================================================
 
  tFilterSave(argCol, 'load');    // �t�B���^�����̕���
  tFilterCloseOpen('');           // �t�B���^���X�g�����
 
}

function tFilterGo(){
 //===============================================================
 //  �t�B���^�̎��s
 //===============================================================
  var wTABLE  = document.getElementById(gTabldID);
  var wTR     = wTABLE.rows;
 
  // ------------------------------------------------------------
  //  �S�Ă̔�\������U�N���A
  // ------------------------------------------------------------
  for(var i = 0; i < wTR.length; i++){
    if(wTR[i].getAttribute('cmanFilterNone') !== null){
      wTR[i].removeAttribute('cmanFilterNone');
    }
  }
 
  // ------------------------------------------------------------
  //  �t�B���^�{�^���̂������J��Ԃ�
  // ------------------------------------------------------------
  for(var wColList = 0; wColList < gTfColList.length; wColList++){
    var wCol       = gTfColList[wColList];
    var wAll       = document.getElementById('tfData_ALL_'+wCol);     // �u���ׂāv�̃`�F�b�N�{�b�N�X
    var wItemSave  = {};
    var wFilterBtn =  document.getElementById('tsBtn_'+wCol);
    var wFilterStr =  document.getElementById('tfInStr_'+wCol);
 
    var wForm      = document.forms['tfForm_'+wCol];
    // -----------------------------------------------------------
    //  �`�F�b�N�{�b�N�X�̐����i�u���ׂāv�̐������j
    // -----------------------------------------------------------
    for (var i = 0; i < wForm.elements.length; i++){
      if(wForm.elements[i].type == 'checkbox'){
        if (wForm.elements[i].checked) {
          wItemSave[wForm.elements[i].value] = 1;      // �`�F�b�N����Ă���l��ۑ�
        }
      }
    }
 
    // -----------------------------------------------------------
    //  �t�B���^�i��\���j�̐ݒ�
    // -----------------------------------------------------------
    if((wAll.checked)&&(trim(wFilterStr.value) == '')){
      wFilterBtn.style.backgroundColor = '';              // �t�B���^�Ȃ��F
    }
    else{
      wFilterBtn.style.backgroundColor = '#ffff00';       // �t�B���^����F
 
      for(var i=gTfStartRow; i < wTR.length; i++){
 
        var wVal = trim(wTR[i].cells[wCol].innerText.toString());
 
        // --- �`�F�b�N�{�b�N�X�I���ɂ��t�B���^ ----------------
        if(!wAll.checked){
          if(wVal in wItemSave){
          }
          else{
            wTR[i].setAttribute('cmanFilterNone','');
          }
        }
 
        // --- ���o�����ɂ��t�B���^ ----------------------------
        if(wFilterStr.value != ''){
          reg = new RegExp(wFilterStr.value);
          if(wVal.match(reg)){
          }
          else{
            wTR[i].setAttribute('cmanFilterNone','');
          }
        }
      }
    }
  }
 
  tFilterCloseOpen('');
}

function tFilterSave(argCol, argFunc){
 //==============================================================
 //  �t�B���^���X�g�̕ۑ��܂��͕���
 //==============================================================
 
  // ---�u���ׂāv�̃`�F�b�N�{�b�N�X�l��ۑ� ------------------
  var wAllCheck = document.getElementById('tfData_ALL_'+argCol);
  if(argFunc == 'save'){
    gTfListSave[wAllCheck.id] = wAllCheck.checked;
  }else{
    wAllCheck.checked = gTfListSave[wAllCheck.id];
  }
 
  // --- �e�`�F�b�N�{�b�N�X�l��ۑ� ---------------------------
  var wForm    = document.forms['tfForm_'+argCol];
  for (var i = 0; i < wForm.elements.length; i++){
    if(wForm.elements[i].type == 'checkbox'){
      if(argFunc == 'save'){
        gTfListSave[wForm.elements[i].id] = wForm.elements[i].checked;
      }else{
        wForm.elements[i].checked = gTfListSave[wForm.elements[i].id];
      }
    }
  }
 
  // --- �܂ޕ����̓��͂�ۑ� ---------------------------------
  var wStrInput = document.getElementById('tfInStr_'+argCol);
  if(argFunc == 'save'){
    gTfListSave[wStrInput.id] = wStrInput.value;
  }else{
    wStrInput.value = gTfListSave[wStrInput.id];
  }
}

function tFilterCloseOpen(argCol){
 //==============================================================
 //  �t�B���^����ĊJ��
 //==============================================================
 
  // --- �t�B���^���X�g����U���ׂĕ��� -----------------------
  for(var i=0; i < gTfColList.length; i++){
    document.getElementById("tfList_"+gTfColList[i]).style.display = 'none';
  }
 
  // --- �w�肳�ꂽ��̃t�B���^���X�g���J�� ---------------------
  if(argCol != ''){
    document.getElementById("tfList_"+argCol).style.display = '';
 
    // --- �t�B���^�����̕ۑ��i�L�����Z�����ɕ������邽�߁j -----
    tFilterSave(argCol, 'save');
 
  }
}

function tFilterAllSet(argCol){
 //==============================================================
 //  �u���ׂāv�̃`�F�b�N��Ԃɍ��킹�āA�e�`�F�b�N��ON/OFF
 //==============================================================
  var wChecked = false;
  var wForm    = document.forms['tfForm_'+argCol];
 
  if(document.getElementById('tfData_ALL_'+argCol).checked){
    wChecked = true;
  }
 
  for (var i = 0; i < wForm.elements.length; i++){
    if(wForm.elements[i].type == 'checkbox'){
      wForm.elements[i].checked = wChecked;
    }
  }
}

function sortNumA(a, b) {
 //==============================================================
 //  �����̃\�[�g�֐��i�����j
 //==============================================================
  a = parseInt(a.replace(/,/g, ''));
  b = parseInt(b.replace(/,/g, ''));
 
  return a - b;
}

function sortStrA(a, b){
 //==============================================================
 //  �����̃\�[�g�֐��i�����j
 //==============================================================
  a = a.toString().toLowerCase();  // �p�啶������������ʂ��Ȃ�
  b = b.toString().toLowerCase();
 
  if     (a < b){ return -1; }
  else if(a > b){ return  1; }
  return 0;
}

function trim(argStr){
 //==============================================================
 //  trim
 //==============================================================
  var rcStr = argStr;
  rcStr	= rcStr.replace(/^[ �@\r\n]+/g, '');
  rcStr	= rcStr.replace(/[ �@\r\n]+$/g, '');
  return rcStr;
}


�y�[�WTOP
