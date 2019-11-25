<template>
  <div :class="className" :id="id" :style="{height:height,width:width}">

  </div>
</template>
<script>
export default {
  props:{
    className:{
      type:String,
      default:''
    },
    id:{
      type:String,
      default:''
    },
    width:{
      type:String,
      default:''
    },
    height:{
      type:String,
      default:''
    }
  },
  data(){
    return {
      vSO:null
    }
  },
  methods:{
    initSelectOption(){

    }
  }
}
</script>
