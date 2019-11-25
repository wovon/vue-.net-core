<template>
  <div class="app-container calendar-list-container">
    <div class="filter-container">
      <el-input @keyup.enter.native="handleFilter" style="width: 200px;" class="filter-item" placeholder="关键词" v-model="listQuery.KeyWorld"></el-input>
      <el-select @change="handleFilter" style="width: 160px" class="filter-item" v-model="listQuery.PrimaryKey">
        <el-option v-for="item in primaryKeyOptions" :key="item.key" :label="item.label" :value="item.key"></el-option>
      </el-select>
      <el-select @change="handleFilter" style="width: 140px" class="filter-item" v-model="listQuery.Order">
        <el-option v-for="item in orderOptions" :key="item.key" :label="item.label" :value="item.key"></el-option>
      </el-select>
      <el-button class="filter-item" type="primary" style="margin-left:3px" v-waves icon="el-icon-search" @click="handleFilter">筛选</el-button>
    </div>
    <div class="filter-container">
      <el-button class="filter-item" v-for="b in buttons" @click="handleElement(b.code)" :key="b.Id" :icon="b.Icon" type="primary">{{b.name}}</el-button>
    </div>
    <el-table :key="tableKey" highlight-row ref="userListTable" :data="list" v-loading="listLoading" @on-current-change="handleRowChange"
      @selection-change="handleRowChange" element-loading-text="给我一点时间" border fit highlight-current-row style="width: 100%">
      <el-table-column type="selection" width="55"></el-table-column>
      <el-table-column align="center" label="序号" width="65">
        <template slot-scope="scope">
          <span>{{scope.row.rowNum}}</span>
        </template>
      </el-table-column>
      <el-table-column width="150px" min-width="100px" label="用户名">
        <template slot-scope="scope">
          <span>{{scope.row.account}}</span>
        </template>
      </el-table-column>
      <el-table-column width="150px" min-width="100px" label="姓名">
        <template slot-scope="scope">
          <span>{{scope.row.name}}</span>
        </template>
      </el-table-column>
      <el-table-column width="80px" align="center" label="性别">
        <template slot-scope="scope">
          <span>{{scope.row.sex | sexFilter}}</span>
        </template>
      </el-table-column>
      <el-table-column width="300px" align="center" label="所属部门">
        <template slot-scope="scope">
          <span style="color:red;">{{scope.row.departmentNames}}</span>
        </template>
      </el-table-column>
      <el-table-column width="300px" align="center" label="所属角色">
        <template slot-scope="scope">
          <span style="color:red;">{{scope.row.roleNames}}</span>
        </template>
      </el-table-column>
      <el-table-column width="120px" align="center" label="添加时间" class-name="small-padding fixed-width">
        <template slot-scope="scope">
          <span>{{scope.row.createTime | parseTime('{y}-{m}-{d}')}}</span>
        </template>
      </el-table-column>
    </el-table>

    <div class="pagination-container">
      <el-pagination background @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="listQuery.PageIndex"
        :page-sizes="[2,5,10,20,30, 50]"
        :page-size="listQuery.PageSize"
        layout="total, sizes, prev, pager, next, jumper" :total="total"></el-pagination>
    </div>

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form :rules="rules" ref="dataForm" :model="temp" label-position="left" label-width="80px" style="width: 400px; margin-left:100px;">
        <el-form-item label="用户名：" prop="account">
          <el-input v-model="temp.account"></el-input>
        </el-form-item>
        <el-form-item label="姓名：" prop="name">
          <el-input v-model="temp.name"></el-input>
        </el-form-item>
        <el-form-item label="性别：" prop="sex">
          <el-select class="filter-item" v-model="temp.sex" placeholder="请选择">
            <el-option v-for="item in sexOptions" :key="item.key" :label="item.label" :value="item.key"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="电话：" prop="tel">
          <el-input v-model="temp.tel"></el-input>
        </el-form-item>
        <el-form-item label="简介：">
          <el-input type="textarea" :autosize="{ minRows: 4, maxRows: 6}" placeholder v-model="temp.description"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取消</el-button>
        <el-button v-if="dialogStatus==='create'" type="primary" @click="createData">添加</el-button>
        <el-button v-else-if="dialogStatus==='edit'" type="primary" @click="editData">修改</el-button>
        <el-button v-else type="primary" @click="dialogFormVisible = false">确定</el-button>
      </div>
    </el-dialog>
    <!-- 弹出层（设置角色） -->
    <el-dialog title="设置角色" :visible.sync="dialogSetRoleVisible">
      <el-form ref="deptFrom" :model="setRoleDepartmentTemp" label-position="left" label-width="80px" style="width: 400px; margin-left:100px;">
        <el-form-item label="名称" >
          <el-input v-model="setRoleDepartmentTemp.name" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="角色" >
          <treeselect :options="roleData" v-model="setRoleDepartmentTemp.roles" :multiple="true" :disable-branch-nodes="false" :show-count="false" placeholder="" :flat="true"
          :defaultExpandLevel="Infinity" :maxHeight="200" style="width:450px;" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogSetRoleVisible=false" >取消</el-button>
        <el-button @click="handleSetRole();dialogSetRoleVisible=false" type="primary">确认</el-button>
      </div>
    </el-dialog>

    <!-- 弹出层（设置部门） -->
    <el-dialog title="设置角色" :visible.sync="dialogSetDepartmentVisible">
      <el-form ref="deptFrom" :model="setRoleDepartmentTemp" label-position="left" label-width="80px" style="width: 400px; margin-left:100px;">
        <el-form-item label="名称" >
          <el-input v-model="setRoleDepartmentTemp.name" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="部门" >
          <treeselect :options="roleData" v-model="setRoleDepartmentTemp.departments" :multiple="true" :disable-branch-nodes="false" :show-count="false" placeholder="" :flat="true"
          :defaultExpandLevel="Infinity" :maxHeight="200" style="width:450px;" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogSetDepartmentVisible=false" >取消</el-button>
        <el-button @click="handleSetDepartment();dialogSetDepartmentVisible=false" type="primary">确认</el-button>
      </div>
    </el-dialog>

    <!-- 删除提示 -->
    <el-dialog title="提示" :visible.sync="dialogConfirm" style="width:666px;margin:0 auto;">
      <span>{{confirmData.content}}</span>
      <span slot="footer" class="dialog-footer">
        <el-button @click="dialogConfirm = false">取消</el-button>
        <el-button type="primary" @click="handleDelete();dialogConfirm = false">确定</el-button>
      </span>
    </el-dialog>

  </div>
</template>

<script>
//import { fetchList, fetchPv, createArticle, updateArticle } from '@/api/article'
import waves from "@/directive/waves"; // 水波纹指令
import { parseTime } from "@/utils";
import {userList, userAdd, userDelete, userEdit, fetchPv, updateArticle} from "@/api/system/users";
import {roleList} from "@/api/system/roles"
import {deptList} from "@/api/system/departments"
import {userDepartmentRoleList} from "@/api/system/departmentRole"
import {buttonListByMenuIds,buttonListByMenuName} from "@/api/system/buttons"
import {userRoleAdd} from "@/api/system/userRole"
import {userDepartmentAdd} from "@/api/system/userDepartment"
import {menuNoCascadeList} from "@/api/system/menus"

// 下拉树
import Treeselect from '@riophae/vue-treeselect'
import '@riophae/vue-treeselect/dist/vue-treeselect.css'

export default {
  name: "complexTable",
  directives: {
    waves
  },
  components:{Treeselect},
  data() {
    return {
      tableKey: 0,
      list: null,
      total: null,
      listLoading: true,
      listQuery: {
        PageIndex: 1,
        PageSize: 20,
        KeyWorld: undefined,
        type: undefined,
        PrimaryKey: "createTime",
        Order: "desc"
      },
      rowSelect: [],
      primaryKeyOptions: [
        { label: "通过Id排序", key: "Id" },
        { label: "通过添加时间排序", key: "createTime" },
        { label: "通过修改时间排序", key: "updateTime" }
      ],
      orderOptions: [
        { label: "正序", key: "asc" },
        { label: "倒序", key: "desc" }
      ],
      sexOptions: [{ label: "男", key: 1 }, { label: "女", key: 0 }],
      buttons: [],
      temp: {
        id: undefined,
        account: "",
        name: "",
        sex: 1,
        description: "",
        tel: ""
      },
      dialogFormVisible: false,//添加修改查看层是否显示
      dialogSetRoleVisible:false,//设置角色层是否显示
      dialogSetDepartmentVisible:false,//设置部门层是否显示
      dialogStatus: "",
      textMap: {edit: "修改",create: "添加",see: "查看"},
      dialogPvVisible: false,
      pvData: [],
      dialogConfirm:false,
      confirmData: {title:"",content:"确定要删除这条数据？"},
      rules: {
        account: [{ required: true, message: "请填写用户名", trigger: "blur" }],
        name: [{ required: true, message: "请填写姓名", trigger: "blur" }],
        tel: [{ required: true, message: "请填写电话号码", trigger: "blur" }]
      },
      downloadLoading: false,
      roleData:[],//角色列表
      setRoleDepartmentTemp:{//角色实体
        ids:[],
        name:"",
        roles:[],
        departments:[],
        data:[]
      }
    };
  },
  filters: {
    sexFilter(sex) {
      if (sex == 1) {
        return "男";
      } else {
        return "女";
      }
    }
  },
  created() {
    this.getButttons()
    this.getList()
    this.getRoleList()
    this.listLoading = false
  },
  methods: {
    //清空实体
    clearRoleDepartmentTemp(){//角色实体
      this.setRoleDepartmentTemp = {
        ids:[],
        name:"",
        roles:[],
        departments:[],
        data:[]
      }
    },
    //获取操作按钮
    // getButttons() {
    //   this.buttons = [
    //     {id: 1,name: "添加",code: "add",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
    //     {id: 2,name: "删除",code: "delete",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
    //     {id: 3,name: "修改",code: "edit",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
    //     {id: 4,name: "查看",code: "see",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
    //     // {id: 5,name: "审批",code: "examine",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
    //     {id: 6,name: "设置角色",code: "setRole",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
    //     // {id: 7,name: "设置部门",code: "setDepartment",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000}
    //   ];
    // },
    getButttons() {
      let _Queery = {name:this.$route.name,component:this.$route.path.substr(1)}
      buttonListByMenuName(_Queery).then(response => {
          this.buttons = response.data.data
      })
    },
    //列表数据
    getList() {
      userList(this.listQuery).then(response => {
        //console.log('tag', response)
        this.list = response.data.items;
        this.total = response.data.total;
      });
    },
    //获取角色下拉
    getRoleList(){
      roleList().then(response=>{
        this.roleData = response.data.data
      }).catch(e=>{

      })
    },
    //通过用户ID获取部门和角色
    getUserDepartmentRoleList(s){
      userDepartmentRoleList(s).then(response=>{
        //console.log(response.data);
        let ids = []
        response.data.data.forEach(row=>{
          let isAddRole = true
          this.setRoleDepartmentTemp.roles.forEach(element => {
            if(element===row.roleId)
              isAddRole = false
          });
          if(isAddRole){
            this.setRoleDepartmentTemp.roles.push(row.roleId)
          }
          //
          let isAddDepartment = true
          this.setRoleDepartmentTemp.departments.forEach(element => {
            if(element===row.departmentId)
              isAddDepartment = false
          });
          if(isAddDepartment){
            this.setRoleDepartmentTemp.departments.push(row.departmentId)
          }
        })
        this.dialogSetRoleVisible = true
      }).catch(e=>{

      })
    },
    //通过用户ID获取部门
    getUserDepartmentList(s){
      userDepartmentRoleList(s).then(response=>{
        //console.log(response.data);
        var ids = []
        response.data.data.forEach(row=>{
          this.setRoleDepartmentTemp.roles.push(row.roleId)
          this.setRoleDepartmentTemp.departments.push(row.departmentId)

        })
        this.dialogSetRoleVisible = true
        //this.setRoleDepartmentTemp.roles = ids
      }).catch(e=>{

      })
    },
    //选中行事件
    handleRowChange(currentRow, oldCurrentRow) {
      this.rowSelect = currentRow
    },
    handleFilter() {
      this.listQuery.PageIndex = 1
      this.getList()
    },
    handleSizeChange(val) {
      this.listQuery.PageSize = val
      this.getList()
    },
    handleCurrentChange(val) {
      this.listQuery.PageIndex = val
      this.getList()
    },
    //重置单条数据
    resetTemp() {
      this.temp = {
        id: undefined,
        account: "",
        name: "",
        sex: 1,
        description: "",
        tel: ""
      }
    },
    //按钮通用事件
    handleElement(code) {
      code = code.toLocaleLowerCase()
      if (code === "add") {
        this.$router.push({path:"/system/vtest",query:{}})
        //this.handleCreate()
      }
      else if (code === "delete") {
        if (this.rowSelect.length > 0) {
          this.dialogConfirm=true
          this.confirmData.content = '你确定要删除这'+this.rowSelect.length+'条数据？';
        }
        else{
          this.$notify({title: "提示",message: "请选择数据！",type: "info",duration: 2000});
        }
      }
      //修改
      else if (code === "edit") {this.handleEdit()}
      //查看
      else if (code === "see") {this.handleSee()}
      //设置角色
      else if(code==="setrole"){
        this.clearRoleDepartmentTemp()
        let _ids = ""
        this.rowSelect.forEach((row,index)=>{
          this.setRoleDepartmentTemp.name += row.name+","
          _ids+=row.id+","
          this.setRoleDepartmentTemp.ids.push(row.id)
        })
        this.getUserDepartmentRoleList({userIds:_ids})
      }
      else if(code==="setdepartment"){
        //deptList
        this.clearRoleDepartmentTemp()
        let _ids = ""
        this.rowSelect.forEach((row,index)=>{
          this.setRoleDepartmentTemp.name += row.name+","
          _ids+=row.id+","
          this.setRoleDepartmentTemp.ids.push(row.id)
        })
        this.getUserDepartmentRoleList({userIds:_ids})
      }
    },
    //添加
    handleCreate(code) {
      //this.resetTemp();
      this.dialogStatus = "create"
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate()
      });
    },
    //添加调用api
    createData() {
      this.$refs["dataForm"].validate(valid => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          userAdd(tempData)
            .then(response => {
              this.dialogFormVisible = false
              if (response.data.code === 200) {
                this.$notify({title: "成功",message: response.data.message,type: "success",duration: 2000})
                //重新加载列表
                this.getList()
              } else {
                this.$notify({title: "失败",message: response.data.message,type: "error",duration: 2000})
              }
            })
            .catch(e => {
              this.$notify({title: "失败",message: "添加失败！catch",type: "error",duration: 2000})
            });
        }
      });
    },
    //删除
    handleDelete() {
      let _ids = { ids: "" }
      this.rowSelect.forEach(element => {
        _ids.ids += element.id + ","
      });
      //判断选择的数据是否生成ID
      if (_ids.ids != "") {
        //调用删除
        userDelete(_ids)
          .then(response => {
            if (response.data.code === 200) {
              this.$notify({title: "成功",message: "删除成功",type: "success",duration: 2000})
              //重新加载列表
              this.getList()
            }
            else
            {
              this.$notify({title: "失败",message: "删除失败！",type: "error",duration: 2000})
            }
          })
          .catch(e => {
            this.$notify({title: "失败",message: "删除失败！",type: "error",duration: 2000})
          });
      }
    },
    //修改
    handleEdit() {
      if (this.rowSelect.length > 1) {
        this.$notify({title: "失败",message: "不支持多条修改",type: "error",duration: 2000})
        this.$refs.userListTable.clearSelection();
      } else if (this.rowSelect.length == 0) {
        this.$notify({title: "提示",message: "请选择要修改的数据",type: "info",duration: 2000});
      } else {
        this.temp = Object.assign({}, this.rowSelect[0]) // copy obj
        this.dialogStatus = "edit"
        this.dialogFormVisible = true
        this.$nextTick(() => {
          this.$refs["dataForm"].clearValidate()
        });
      }
    },
    //修改调用api
    editData() {
      this.$refs["dataForm"].validate(valid => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          userEdit(tempData)
            .then(response => {
              if (response.data.code === 200){
                this.$notify({title: "成功",message: response.data.message,type: "success",duration: 2000})
                this.$refs.userListTable.clearSelection()
                this.dialogFormVisible = false
                this.getList()
              }
              else
              {
                this.$notify({title: "失败",message: response.data.message,type: "error",duration: 2000})
              }
            })
            .catch(e => {
              this.$notify({title: "失败",message: "修改失败！catch",type: "error",duration: 2000})
            });
        }
      });
    },
    //查看
    handleSee() {
      if (this.rowSelect.length > 1) {
        this.$notify({title: "失败",message: "不支持多条查看",type: "error",duration: 2000})
        this.$refs.userListTable.clearSelection()
      } else if (this.rowSelect.length == 0) {
        this.$notify({title: "提示",message: "请选择要查看的数据",type: "info",duration: 2000})
      } else {
        this.temp = Object.assign({}, this.rowSelect[0]) // 拷贝对象
        this.dialogStatus = "see"
        this.dialogFormVisible = true
        this.$nextTick(() => {
          this.$refs["dataForm"].clearValidate()
        });
      }
    },
    handleSetRole(){
      // this.$refs['dataForm'].validate(valid=>{
      //   if(valid){
          const tempData = []
          this.setRoleDepartmentTemp.ids.forEach(userId => {
            let _temp={}
            _temp.userId=userId
            this.setRoleDepartmentTemp.roles.forEach(roleId => {
              _temp.roleId = roleId
              tempData.push(_temp)
            });
            if(tempData.length===0)
            {
              _temp.roleId = ''
              tempData.push(_temp)
            }
          });
          console.log('setRole',tempData)
          userRoleAdd(tempData).then(response=>{
            //console.log(response)
            this.getList()
          })
          // else
          // {
          //   this.$notify({title:"提示",message:"请选择角色",type:"info",duration:2000})
          // }
      //   }
      // })
    },
    handleSetDepartment(){
      // this.$refs['dataForm'].validate(valid=>{
      //   if(valid){
          const tempData = []
          this.setRoleDepartmentTemp.ids.forEach(userId => {
            this.setRoleDepartmentTemp.roles.forEach(roleId => {
              let _temp={}
              _temp.userId=userId
              _temp.roleId = roleId
              tempData.push(_temp)
            });
          });
          userDepartmentAdd(tempData).then(response=>{
            //console.log(response)
            this.getList()
          })
          // else
          // {
          //   this.$notify({title:"提示",message:"请选择角色",type:"info",duration:2000})
          // }
      //   }
      // })
    }
  }
};
</script>
