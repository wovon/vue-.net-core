<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button class="filter-item" v-for="b in buttons" @click="handleElement(b.code)" :key="b.Id" :icon="b.Icon" type="primary">{{b.name}}</el-button>
    </div>
    <el-table :key="tableKey" highlight-row ref="workflowListTable" :data="list" v-loading="listLoading" @on-current-change="handleRowChange"
      @selection-change="handleRowChange"  element-loading-text="给我一点时间"  border fit highlight-current-row style="width: 100%">
      <el-table-column type="selection" width="55"></el-table-column>
      <el-table-column width="150px" min-width="100px" label="流程名">
        <template slot-scope="scope">
          <span>{{scope.row.name}}</span>
        </template>
      </el-table-column>
      <el-table-column width="150px" min-width="100px" label="流程标识">
        <template slot-scope="scope">
          <span>{{scope.row.identity}}</span>
        </template>
      </el-table-column>
      <el-table-column width="150px" min-width="100px" label="SQLID">
        <template slot-scope="scope">
          <span>{{scope.row.sqlId}}</span>
        </template>
      </el-table-column>
      <el-table-column width="150px" min-width="100px" label="备注">
        <template slot-scope="scope">
          <span>{{scope.row.remark}}</span>
        </template>
      </el-table-column>
      <el-table-column width="150px" min-width="100px" label="是否启用">
        <template slot-scope="scope">
          <span>{{scope.row.isAble}}</span>
        </template>
      </el-table-column>
      <el-table-column width="150px" min-width="100px" label="是否删除">
        <template slot-scope="scope">
          <span>{{scope.row.isDel}}</span>
        </template>
      </el-table-column>
      <el-table-column width="150px" min-width="100px" label="添加时间">
        <template slot-scope="scope">
          <span>{{scope.row.createTime | parseTime('{y}-{m}-{d}')}}</span>
        </template>
      </el-table-column>
      <el-table-column width="150px" min-width="100px" label="排序">
        <template slot-scope="scope">
          <span>{{scope.row.sort}}</span>
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
      <el-form ref="workflowForm" :model="temp" label-position="left" label-width="80px" style="width: 400px; margin-left:100px;">
        <el-form-item label="流程名称" prop="name">
          <el-input v-model="temp.name"></el-input>
        </el-form-item>
        <el-form-item label="流程标识" prop="code">
          <el-input v-model="temp.identity"></el-input>
        </el-form-item>
        <el-form-item label="流程备注" prop="label">
          <el-input v-model="temp.remark"></el-input>
        </el-form-item>
        <el-form-item label="排  序">
          <el-input-number v-model="temp.sort" :min="1" :max="10000" label=""></el-input-number>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取消</el-button>
        <el-button v-if="dialogStatus==='add'" type="primary" @click="addData">添加</el-button>
        <el-button v-if="dialogStatus==='edit'" type="primary" @click="editData">修改</el-button>
        <el-button v-if="dialogStatus=='see'" @click="dialogFormVisible=false" type="primary">确认</el-button>
      </div>
    </el-dialog>




    <!-- 删除提示 -->
    <el-dialog title="提示" :visible.sync="dialogConfirm" style="width:666px;margin:0 auto;">
      <span>{{confirmData.content}}</span>
      <span slot="footer" class="dialog-footer">
        <el-button @click="dialogConfirm = false">取消</el-button>
        <el-button type="primary"  @click="handleDelete();dialogConfirm = false">确定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
  import {buttonListByMenuIds,buttonListByMenuName} from "@/api/system/buttons"
  import {menuNoCascadeList} from "@/api/system/menus"
  import {workflowList,workflowAdd,workflowEdit,workflowDelete} from "@/api/workflow/workflows"
  import { parseTime } from "@/utils";

  export default {
    name:'customWorkflowTable',
    data() {
      return {
        buttons: [],
        tableKey: 0,
        list:[],
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
        temp: {
            id: undefined,
            name: '',
            identity: '',
            remark: '',
            label:'',
            isDel:false,
            isAble:false,
            workOrderManagementId:'',
            createTime: new Date(),
            updateTime: new Date(),
            sort:1000
        },
        dialogFormVisible: false,//添加修改查看层是否显示
        dialogStatus: "",
        textMap: {edit: "修改",create: "添加",see: "查看"},
        confirmData: {title:"",content:"确定要删除这条数据？"},
        dialogConfirm:false,
        rowSelect: [],
      }
    },
    created(){
      this.getButtons()
      this.getList()
      this.listLoading = false
    },
    methods: {
      getButtons()
      {
        this.buttons = [
              {id: 1,name: "添加",code: "add",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
              {id: 2,name: "删除",code: "delete",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
              {id: 3,name: "修改",code: "edit",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
              {id: 4,name: "查看",code: "see",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
              // {id: 5,name: "设置角色",code: "setRole",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
              // {id: 6,name: "全部展开",code: "expandall",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
              // {id: 7,name: "全部收缩",code: "collapseall",icon: "el-icon-edit",explain: "", createTime: "",updateTime: "",sort: 1000}
            ];
        // let _Queery = {name:this.$route.name,component:this.$route.path.substr(1)}
        // buttonListByMenuName(_Queery).then(response => {
        //   this.buttons = response.data.data
        // })
      },
      getList(){
        this.listLoading = true;
        workflowList(this.listQuery).then(response=>{
          this.list = response.data.data
          this.total = response.data.total
          this.listLoading = false
        })
      },
      //列表选中事件
      handleRowChange(currentRow, oldCurrentRow) {
        this.rowSelect = currentRow
      },
      //列表分页事件
      handleSizeChange(val) {
        this.listQuery.PageSize = val
        this.getList()
      },
      //列表分页事件
      handleCurrentChange(val) {
        this.listQuery.PageIndex = val
        this.getList()
      },
      //重置单条数据
        resetTemp() {
            this.temp = {
                id: '',
                name: '',
                identity: '',
                remark: '',
                label:'',
                isDel:false,
                isAble:false,
                workOrderManagementId:'',
                createTime: new Date(),
                updateTime: new Date(),
                sort:1000
            }
        },
      //按钮通用事件
      handleElement(code) {
          code = code.toLocaleLowerCase()
          if (code === "add") {
              this.handleAdd()
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
        },
        //添加
        handleAdd() {
            this.resetTemp()
            this.dialogStatus = 'add'
            this.dialogFormVisible = true
            this.$nextTick(() => {
                this.$refs['workflowForm'].clearValidate()
            })
        },
        addData() {
            this.$refs["workflowForm"].validate(valid => {
            if (valid) {
                const tempData = Object.assign({}, this.temp)
                console.log(tempData)
                workflowAdd(tempData)
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
            let _codes = { codes:"" }
            this.rowSelect.forEach(element => {
                _ids.ids += element.id + ",";
                _codes.codes += element.code + ",";
            });
            //判断选择的数据是否生成ID
            if (_ids.ids != "" && _codes.codes.indexOf("add")<0 && _codes.codes.indexOf("edit")<0 && _codes.codes.indexOf("delete")<0 && _codes.codes.indexOf("see")<0) {
                //调用删除
                workflowDelete(_ids)
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
                this.$refs.workflowListTable.clearSelection();
            } else if (this.rowSelect.length == 0) {
                this.$notify({title: "提示",message: "请选择要修改的数据",type: "info",duration: 2000});
            } else {
                this.temp = Object.assign({}, this.rowSelect[0]) // copy obj
                this.dialogStatus = "edit"
                this.dialogFormVisible = true
                this.$nextTick(() => {
                    this.$refs["workflowForm"].clearValidate()
                });
            }
        },
            //修改调用api
        editData() {
            this.$refs["workflowForm"].validate(valid => {
                if (valid) {
                    const tempData = Object.assign({}, this.temp)
                    workflowEdit(tempData)
                        .then(response => {
                        if (response.data.code === 200){
                            this.$notify({title: "成功",message: response.data.message,type: "success",duration: 2000})
                            this.$refs.workflowListTable.clearSelection()
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
                this.$refs.workflowListTable.clearSelection()
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

    }
}
</script>
