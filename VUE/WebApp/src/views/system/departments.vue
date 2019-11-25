<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button class="filter-item" v-for="b in buttons" @click="handleElement(b.code)" :key="b.Id" :icon="b.Icon" type="primary">{{b.name}}</el-button>
    </div>
    <tree-table :data="treeData" ref="deptTable" :evalFunc="func" @func="getRowSelect"  :evalArgs="args" :expandAll="expandAll" border >
      <el-table-column label="排序">
        <template slot-scope="scope">
          <span>{{scope.row.sort}}</span>
        </template>
      </el-table-column>
      <!-- <el-table-column label="设置对应子类的ID">
        <template slot-scope="scope">
          <span>{{scope.row.useChildId}}</span>
        </template>
      </el-table-column> -->
      <el-table-column label="拥有角色">
        <template slot-scope="scope">
          <span style="color:red;">{{scope.row.roleNames}}</span>
        </template>
      </el-table-column>
      <el-table-column label="说明">
        <template slot-scope="scope">
          <span>{{scope.row.explain}}</span>
        </template>
      </el-table-column>
      <el-table-column label="添加时间">
        <template slot-scope="scope">
          <span>{{scope.row.createTime | parseTime}}</span>
        </template>
      </el-table-column>
      <!-- <el-table-column label="修改时间">
        <template slot-scope="scope">
          <span>{{scope.row.updateTime | parseTime}}</span>
        </template>
      </el-table-column> -->
    </tree-table>

    <!-- 弹出层（添加，修改，查看） -->
    <el-dialog :title="dialogMap[dialogStatus]" :visible.sync="dialogFromVisible">
      <el-form ref="deptFrom" :model="temp" label-position="right" label-width="80px" style="width: 400px; margin-left:100px;">
        <el-form-item label="名  称" >
          <el-input v-model="temp.name" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="上一级" >
          <!-- <treeselect :options="treeData" :multiple="multiple" v-model="temp.parentId" :disable-branch-nodes="false" :show-count="false" placeholder="" :flat="true" :defaultExpandLevel="Infinity" :maxHeight="200" style="width:450px;" /> -->
          <treeselect
          :multiple="true"
          :options="treeData"
          :disable-branch-nodes="false"
          v-model="temp.parentId"
          :value="temp.parentId"
          :flat="true" :defaultExpandLevel="Infinity" :maxHeight="200"
          search-nested
          placeholder=""
          style="width:450px;"
          noChildrenText = ''
          />
        </el-form-item>
        <!-- <el-form-item label="设置对应子类的ID" >
          <treeselect :options="treeData" v-model="temp.useChildId" :disable-branch-nodes="false" :show-count="false" placeholder="" :flat="true" :defaultExpandLevel="Infinity" :maxHeight="200" style="width:450px;" />
        </el-form-item> -->
        <el-form-item label="排  序">
          <el-input-number v-model="temp.sort" :min="1" :max="10000" label=""></el-input-number>
        </el-form-item>
        <el-form-item label="说  明">
          <el-input type="textarea" :autosize="{ minRows: 4, maxRows: 8}" placeholder="请输入内容" v-model="temp.explain" style="width:450px">
          </el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFromVisible=false" >取消</el-button>
        <el-button v-if="dialogStatus=='add'" @click="handleAdd" type="primary">添加</el-button>
        <el-button v-if="dialogStatus=='edit'" @click="handleEidt" type="primary">修改</el-button>
        <el-button v-if="dialogStatus=='see'" @click="dialogFromVisible=false" type="primary">确认</el-button>
      </div>
    </el-dialog>
    <!-- 弹出层（设置角色） -->
    <el-dialog title="设置角色" :visible.sync="dialogSetRoleVisible">
      <el-form ref="deptFrom" :model="temp" label-position="left" label-width="80px" style="width: 400px; margin-left:100px;">
        <el-form-item label="名称" >
          <el-input v-model="setRoleTemp.name" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="上一级" >
          <treeselect :options="roleData" v-model="setRoleTemp.roles" :multiple="true" :disable-branch-nodes="false" :show-count="false" placeholder="" :flat="true"
          :defaultExpandLevel="Infinity" :maxHeight="200" style="width:450px;" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogSetRoleVisible=false" >取消</el-button>
        <el-button @click="handleSetRole();dialogSetRoleVisible=false" type="primary">确认</el-button>
      </div>
    </el-dialog>
    <!-- 删除提示 -->
    <el-dialog title="提示" :visible.sync="dialogConfirm" style="width:666px;margin:0 auto;">
      <span v-html="confirmData.content"></span>
      <span slot="footer" class="dialog-footer">
        <el-button @click="dialogConfirm = false">取消</el-button>
        <el-button type="primary" @click="handleDelete();dialogConfirm = false">确定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
/**
  Auth: v
  Created: 2019-01-09 14:54
*/
import treeTable from '@/components/TreeTable'
import treeToArray from '@/views/example/table/treeTable/customEval'
import {deptList,deptAdd,deptDelete,deptEdit,setDepartmentRole} from "@/api/system/departments"
import {roleList} from "@/api/system/roles"
import {departmentRoleList} from "@/api/system/departmentRole"
import {buttonListByMenuIds,buttonListByMenuName} from "@/api/system/buttons"
import { parseTime } from "@/utils";
import {menuNoCascadeList} from "@/api/system/menus"

// 下拉树
import Treeselect from '@riophae/vue-treeselect'
import '@riophae/vue-treeselect/dist/vue-treeselect.css'

export default {
  name: 'customDeptTable',
  components: { treeTable,Treeselect },
  data() {
    return {
      func: treeToArray,
      testSelect:["5D082DF5-A11F-43A4-96DE-BD7043433542","7A648A3F-2F7C-4616-8852-5D1FCB13F1BE"],
      expandAll: true,
      treeData:[],
      buttons:[],
      roleData:[],
      args: [null, null, ''],
      rowSelect: [],
      dialogSetRoleVisible:false,
      dialogConfirm:false,
      confirmData: {title:"",content:"确定要删除这条数据？"},
      dialogFromVisible:false,
      dialogStatus:"",
      dialogMap:{add:"添加",delete:"删除",edit:"修改",see:"查看",setRole:"设置角色"},
      temp:{
        id:'',
        name:'',
        label:'',
        parentId:[],
        parentName:'',
        cascadeId:'',
        useChildId:undefined,
        explain:'',
        createTime:new Date(),
        updateTime:new Date(),
        isAble:false,
        sort:1000
      },
      setRoleTemp:{
        name:"",
        roles:[],
        data:[]
      }
    }
  },
  created(){
    this.getButttons()
    this.getDeptList()
    this.getRoleList()
  },
  // computed:{
  //   setRoleTemp:{
  //     get:function(){},
  //     set:function(value){
  //       this.name = value.trim(',')
  //     }
  //   }
  // },
  methods: {//获取操作按钮
    // getButttons() {
    //   this.buttons = [
    //     {id: 1,name: "添加",code: "add",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
    //     {id: 2,name: "删除",code: "delete",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
    //     {id: 3,name: "修改",code: "edit",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
    //     {id: 4,name: "查看",code: "see",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
    //     {id: 5,name: "设置角色",code: "setRole",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
    //     // {id: 6,name: "全部展开",code: "expandall",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
    //     // {id: 7,name: "全部收缩",code: "collapseall",icon: "el-icon-edit",explain: "", createTime: "",updateTime: "",sort: 1000}
    //   ];
    // },
    getButttons() {
      let _Queery = {name:this.$route.name,component:this.$route.path.substr(1)}
      buttonListByMenuName(_Queery).then(response => {
          this.buttons = response.data.data
      })
    },
    message(row) {
      this.$message.info(row.event)
    },
    getRowSelect(rows){
      this.rowSelect = rows
    },
    getDeptList(){
      deptList().then(response=>{
        console.log('deptList', response.data.data)
        console.log(JSON.stringify(response.data.data))
        this.treeData = response.data.data;
      }).catch((e)=>{
        console.log(e)
      })
    },
    getRoleList(){
      roleList().then(response=>{
        var resData = response.data.data
        this.roleData = response.data.data
      }).catch(e=>{

      })
    },
    getDepartmentRoleList(s){
      departmentRoleList(s).then(response=>{
        var ids = []
        response.data.data.forEach(row=>{
          ids.push(row.id)
        })
        this.setRoleTemp.roles = ids

      }).catch(e=>{

      })
    },
    clearRoleTemp(){
      this.setRoleTemp = {
        name:"",
        departments:[],
        roles:[],
        data:[]
      }
    },
    handleElement(code){
      code = code.toLocaleLowerCase()
      this.dialogStatus = code;
      if(code==="expandall")
      {
        this.expandAll = true
      }
      else if(code==="collapseall")
      {
        this.expandAll = false
      }
      //添加
      else if(code==="add")
      {
        this.dialogFromVisible = true
      }
      //删除
      else if(code==="delete")
      {
        if(this.rowSelect.length==0)
        {
          this.$notify({title:"提示",message:"请选择要删除的数据",type:"info",duration:2000})
        }
        else if (this.rowSelect.length > 0) {
          this.dialogConfirm=true
          this.confirmData.content = '你确定要删除这'+this.rowSelect.length+'条数据？删除之后相关联的下级也将删除。<font color="red">如有共用下级情况，对应下级也将删除。请谨慎操作。</font>'
        }
      }
      // 修改
      else if(code==="edit" || code==="see")
      {

        if(this.rowSelect.length===0)
        {
          this.$notify({title: "提示",message: "请选择要"+this.dialogMap[this.dialogStatus]+"的数据",type: "info",duration: 2000})
        }
        else if(this.rowSelect.length>1)
        {
          this.$notify({title:"提示",message:"不支持多条数据"+this.dialogMap[this.dialogStatus],type:"info",duration:2000})
          this.$refs.deptTable.handleClearSelection()
        }
        else
        {
          this.temp = Object.assign({},this.rowSelect[0])
          delete this.temp.parent
          delete this.temp.children
          this.temp.parentId = this.temp.parentId!=undefined?this.temp.parentId.split(','):[];
          this.dialogFromVisible = true
          this.$nextTick(() => {
          this.$refs["deptFrom"].clearValidate()
        });
        }
      }
      //设置角色
      else if(code=="setrole")
      {
        if(this.rowSelect.length===0)
        {
          this.$notify({title: "提示",message: "请选择要"+this.dialogMap[this.dialogStatus]+"的数据",type: "info",duration: 2000})
        }
        else
        {
          var ids = "";
          this.dialogSetRoleVisible = true
          this.clearRoleTemp()
          this.rowSelect.forEach((row,index)=>{
            this.setRoleTemp.name += row.name+","
            this.setRoleTemp.departments.push(row.id)
            ids+=row.id+",";
          })
          this.setRoleTemp.name = this.setRoleTemp.name.replace(/,$/gi,"")
          this.getDepartmentRoleList({departmentIds:ids});
        }
      }
    },
    handleAdd(){
      const tempData = Object.assign({},this.temp)
      tempData.parentId = tempData.parentId!=undefined?tempData.parentId.join(','):'';
      this.$refs["deptFrom"].validate(valid => {
        deptAdd(tempData).then(response=>{
          if(response.data.code===200)
          {
            this.$notify({title: "成功",message: "添加成功！",type: "success",duration: 2000})
            this.dialogFromVisible = false;
            this.getDeptList();
          }
          else{
            this.$notify({title: "失败",message: "添加失败！",type: "error",duration: 2000})
          }
        })
        .catch((e)=>{
          this.$notify({title: "失败",message: "添加失败！catch",type: "error",duration: 2000})
        })
      })
    },
    handleDelete(){
      let _ids={ ids: "" }
      this.rowSelect.forEach((row,index)=>{
        _ids.ids+=row.id+","
      })
      if(_ids.ids!="")
      {
        deptDelete(_ids).then(response=>{
          if(response.data.code===200)
          {
            this.$notify({title: "成功",message: this.dialogMap[this.dialogStatus]+"成功！",type: "success",duration: 2000})
            this.getDeptList();
          }
          else
          {
            this.$notify({title: "失败",message: this.dialogMap[this.dialogStatus]+"失败！",type: "error",duration: 2000})
          }
        })
        .catch(e=>{
          this.$notify({title: "失败",message: this.dialogMap[this.dialogStatus]+"失败！catch",type: "error",duration: 2000})
        })
      }
    },
    handleEidt(){
      const tempData = Object.assign({},this.temp)
      tempData.parentId = tempData.parentId!=undefined?tempData.parentId.join(','):'';

      this.$refs["deptFrom"].validate(valid => {
        deptEdit(tempData).then(response=>{
          if(response.data.code===200)
          {
            this.dialogFromVisible = false
            this.$notify({title:"成功",message:"修改成功",type:"success",duration:2000})
            this.getDeptList()
          }
          else
          {
            this.$notify({title:"失败",message:"修改失败",type:"error",duration:2000})
          }
        })
        .catch(e=>{
          this.$notify({title: "失败",message: "修改失败！catch",type: "error",duration: 2000})
        })
      })
    },
    //设置角色
    handleSetRole(){
      this.setRoleTemp.departments.forEach((row,index)=>{
        this.setRoleTemp.roles.forEach((row2)=>{
          this.setRoleTemp.data.push({departmentId:row,roleId:row2})
        })
        if(this.setRoleTemp.data.length==0)
        {
          this.setRoleTemp.data.push({departmentId:row,roleId:''})
        }
      })
      setDepartmentRole({DepartmentRoles:this.setRoleTemp.data}).then((response)=>{
        if(response.data.code===200)
        {
          this.dialogSetRoleVisible = false
          this.$notify({title:"成功",message:"设置成功",type:"success",duration:2000})
          this.$refs.deptTable.handleClearSelection()
          this.getDeptList()
          this.getRoleList()
        }
        else
        {
          this.$notify({title:"失败",message:"设置失败",type:"error",duration:2000})
        }
      }).catch(e=>{
        this.$notify({title: "失败",message: "设置失败！catch",type: "error",duration: 2000})
      })
    }
  }
}
</script>
