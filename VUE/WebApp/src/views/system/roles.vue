<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button class="filter-item" v-for="b in buttons" @click="handleElement(b.code)" :key="b.Id" :icon="b.Icon" type="primary">{{b.name}}</el-button>
    </div>
    <tree-table :data="roleData" ref="roleTable" :evalFunc="func" @func="getRowSelect"  :evalArgs="args" :expandAll="expandAll" border >
      <el-table-column label="排序">
        <template slot-scope="scope">
          <span>{{scope.row.sort}}</span>
        </template>
      </el-table-column>
      <el-table-column label="对应部门">
        <template slot-scope="scope">
          <span style="color:red;">{{scope.row.departmentNames}}</span>
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
      <el-form ref="roleFrom" :model="temp" label-position="right" label-width="80px" style="width: 400px; margin-left:100px;">
        <el-form-item label="名  称" >
          <el-input v-model="temp.name" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="上一级" >
          <treeselect
          :multiple="true"
          :options="roleData"
          :disable-branch-nodes="false"
          v-model="temp.parentId"
          :value="temp.parentId"
          :flat="true" :defaultExpandLevel="Infinity" :maxHeight="200"
          search-nested
          placeholder=""
          style="width:450px;"
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
    <!-- 弹出层（设置部门） -->
    <el-dialog title="设置部门" :visible.sync="dialogSetRoleVisible">
      <el-form ref="roleFrom" :model="temp" label-position="left" label-width="80px" style="width: 400px; margin-left:100px;">
        <el-form-item label="名称" >
          <el-input v-model="setDepartmentTemp.name" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="部门" >
          <treeselect :options="treeData" v-model="setDepartmentTemp.departments" :value="setDepartmentTemp.departments" :multiple="true" :disable-branch-nodes="false" :show-count="false" placeholder="" :flat="true"
          :defaultExpandLevel="Infinity" :maxHeight="200" style="width:450px;" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogSetRoleVisible=false" >取消</el-button>
        <el-button @click="handleSetDepartment();dialogSetRoleVisible=false" type="primary">确认</el-button>
      </div>
    </el-dialog>

    <!-- 弹出层（设置权限） -->
    <el-dialog title="设置权限" :visible.sync="dialogSetAuthorityVisible">
      <el-form ref="authorrityFrom" :model="temp" label-position="left" label-width="80px" style="width: 400px; margin-left:100px;">
        <el-form-item label="名称" >
          <el-input v-model="setAuthorrityTemp.name" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="权限" >
          <treeselect :options="menuData" v-model="setAuthorrityTemp.buttons" :multiple="true"
          :value-consists-of="valueConsistsOf"
          :disable-branch-nodes="false" :show-count="false" placeholder="" :flat="true"
          :defaultExpandLevel="Infinity" :maxHeight="200" style="width:450px;" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogSetRoleVisible=false" >取消</el-button>
        <el-button @click="handleSetAuthority();dialogSetAuthorityVisible=false" type="primary">确认</el-button>
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
import {deptList} from "@/api/system/departments"
import {roleList,roleAdd,roleDelete,roleEdit,setRoleDepartment} from "@/api/system/roles"
import {roleDepartmentList} from "@/api/system/departmentRole"
import {buttonListByMenuIds,buttonListByMenuName} from "@/api/system/buttons"
import { parseTime } from "@/utils";
import {menuAuthorityList,menuNoCascadeList} from "@/api/system/menus"

// 下拉树
import Treeselect from '@riophae/vue-treeselect'
import '@riophae/vue-treeselect/dist/vue-treeselect.css'

export default {
  name: 'customRoleTable',
  components: { treeTable,Treeselect },
  data() {
    return {
      func: treeToArray,
      expandAll: true,
      treeData:[],
      menuData:[],
      buttons:[],
      roleData:[],
      args: [null, null, ''],
      rowSelect: [],
      valueConsistsOf:"ALL_WITH_INDETERMINATE",
      dialogSetRoleVisible:false,
      dialogSetAuthorityVisible:false,
      dialogConfirm:false,
      confirmData: {title:"",content:"确定要删除这条数据？"},
      dialogFromVisible:false,
      dialogStatus:"",
      dialogMap:{add:"添加",delete:"删除",edit:"修改",see:"查看",setDepartment:"设置部门",setAuthority:"设置权限"},
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
      setDepartmentTemp:{
        name:"",
        department:[],
        data:[]
      }
      ,
      setAuthorrityTemp:{
        name:"",
        buttons:[],
        data:[]
      }
    }
  },
  created(){
    this.getButttons()
    this.getRoleList()
    this.getDepartmentList()
    this.getMenuList()
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
    //     {id: 5,name: "设置部门",code: "setDepartment",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
    //     {id: 5,name: "设置权限",code: "setAuthority",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
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
    //读取部门下拉列表
    getDepartmentList(){
      deptList().then(response=>{
        console.log(response.data.data);
        this.treeData = response.data.data;
      }).catch((e)=>{
        console.log(e)
      })
    },
    //角色列表
    getRoleList(){
      roleList().then(response=>{
        this.roleData = response.data.data
      }).catch(e=>{
      })
    },
    //获取菜单对应按钮
    getMenuList()
    {
      menuAuthorityList().then(response=>{
        console.log(response.data.data)
        this.menuData = response.data.data
      }).catch(e=>{
      })
    },
    //部门对应的角色
    async getRoleDepartmentList(s){
      await roleDepartmentList(s).then(response=>{
        console.log(response);
        var ids = []
        response.data.data.forEach(row=>{
          ids.push(row.id)
        })
        this.setDepartmentTemp.departments = ids
        this.dialogSetRoleVisible = true
      }).catch(e=>{
      })
    },
    clearRoleTemp(){
      this.setDepartmentTemp = {
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
      //修改
      else if(code==="edit" || code==="see")
      {
        if(this.rowSelect.length===0)
        {
          this.$notify({title: "提示",message: "请选择要"+this.dialogMap[this.dialogStatus]+"的数据",type: "info",duration: 2000})
        }
        else if(this.rowSelect.length>1)
        {
          this.$notify({title:"提示",message:"不支持多条数据"+this.dialogMap[this.dialogStatus],type:"info",duration:2000})
          this.$refs.roleTable.handleClearSelection()
        }
        else
        {
          this.temp = Object.assign({},this.rowSelect[0])
          delete this.temp.parent
          delete this.temp.children
          this.temp.parentId = this.temp.parentId!=undefined?this.temp.parentId.split(','):[];
          this.dialogFromVisible = true
          this.$nextTick(() => {
            this.$refs["roleFrom"].clearValidate()
          });
        }
      }
      //设置部门
      else if(code=="setdepartment")
      {
        if(this.rowSelect.length===0)
        {
          this.$notify({title: "提示",message: "请选择要"+this.dialogMap[this.dialogStatus]+"的数据",type: "info",duration: 2000})
        }
        else
        {
          var ids = "";

          this.clearRoleTemp()
          // this.setDepartmentTemp.departments = [];
          this.rowSelect.forEach((row,index)=>{
            this.setDepartmentTemp.name += row.name+","
            this.setDepartmentTemp.roles.push(row.id)
            ids+=row.id+",";
          })
          //this.setDepartmentTemp.departments = ids
          //console.log(this.setDepartmentTemp.departments);
          this.setDepartmentTemp.name = this.setDepartmentTemp.name.replace(/,$/gi,"")
          this.getRoleDepartmentList({roleIds:ids})
          //this.dialogSetRoleVisible = true
        }
      }
      else if(code=="setauthority")
      {
          this.dialogSetAuthorityVisible = true
      }
    },
    handleAdd(){
      const tempData = Object.assign({},this.temp)
      tempData.parentId = tempData.parentId!=undefined?tempData.parentId.join(','):'';
      this.$refs["roleFrom"].validate(valid => {
        roleAdd(tempData).then(response=>{
          if(response.data.code===200)
          {
            this.$notify({title: "成功",message: "添加成功！",type: "success",duration: 2000})
            this.dialogFromVisible = false;
            this.getRoleList();
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
        roleDelete(_ids).then(response=>{
          if(response.data.code===200)
          {
            this.$notify({title: "成功",message: this.dialogMap[this.dialogStatus]+"成功！",type: "success",duration: 2000})
            this.getRoleList();
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

      this.$refs["roleFrom"].validate(valid => {
        roleEdit(tempData).then(response=>{
          if(response.data.code===200)
          {
            this.dialogFromVisible = false
            this.$notify({title:"成功",message:"修改成功",type:"success",duration:2000})
            this.getRoleList()
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
    //设置部门
    handleSetDepartment(){
      this.setDepartmentTemp.roles.forEach((row,index)=>{
        this.setDepartmentTemp.departments.forEach((row2)=>{
          this.setDepartmentTemp.data.push({departmentId:row2,roleId:row})
        })
        if(this.setDepartmentTemp.data.length==0)
        {
          this.setDepartmentTemp.data.push({departmentId:"",roleId:row})
        }
      })
      setRoleDepartment({DepartmentRoles:this.setDepartmentTemp.data}).then((response)=>{
        if(response.data.code===200)
        {
          this.dialogSetRoleVisible = false
          this.$notify({title:"成功",message:"设置成功",type:"success",duration:2000})
          this.$refs.roleTable.handleClearSelection()
          this.getRoleList();
          this.getDepartmentList();
        }
        else
        {
          this.$notify({title:"失败",message:"设置失败",type:"error",duration:2000})
        }
      }).catch(e=>{
        this.$notify({title: "失败",message: "设置失败！catch",type: "error",duration: 2000})
      })
    },
    //dialogSetAuthorityVisible
    handleSetAuthority(){
      console.log(this.setAuthorrityTemp);
      // this.setAuthorrityTemp.roles.forEach((row,index)=>{
      //   this.setDepartmentTemp.departments.forEach((row2)=>{
      //     this.setDepartmentTemp.data.push({departmentId:row2,roleId:row})
      //   })
      //   if(this.setDepartmentTemp.data.length==0)
      //   {
      //     this.setDepartmentTemp.data.push({departmentId:"",roleId:row})
      //   }
      // })
    }
  }
}
</script>
