<template>
    <div class="app-container calendar-list-container">
    <div class="filter-container">
      <el-button class="filter-item" v-for="b in buttons" @click="handleElement(b.code)" :key="b.Id" :icon="b.Icon" type="primary">{{b.name}}</el-button>
    </div>
    <el-table :key="tableKey" highlight-row ref="HistoryListTable" :data="list" v-loading="listLoading" @on-current-change="handleRowChange"
      @selection-change="handleRowChange" element-loading-text="给我一点时间" border fit highlight-current-row style="width: 100%">
      <el-table-column type="selection" width="55"></el-table-column>
      <el-table-column align="center" label="序号" width="100px">
        <template slot-scope="scope">
          <span>{{scope.row.id}}</span>
        </template>
      </el-table-column>
      <el-table-column width="220px" min-width="100px" label="添加人">
        <template slot-scope="scope">
          <span>{{scope.row.userName}}</span>
        </template>
      </el-table-column>
      <el-table-column width="220px" min-width="100px" label="意见">
        <template slot-scope="scope">
          <span>{{scope.row.remake}}</span>
        </template>
      </el-table-column>
      <!-- <el-table-column width="220px" align="center" label="添加时间">
        <template slot-scope="scope">
          <span>{{scope.row.label}}</span>
        </template>
      </el-table-column> -->
      <!-- <el-table-column width="220px" align="center" label="按钮图标">
        <template slot-scope="scope">
          <span>{{scope.row.icon}}</span>
        </template>
      </el-table-column>
      <el-table-column width="220px" align="center" label="按钮说明">
        <template slot-scope="scope">
          <span>{{scope.row.explain}}</span>
        </template>
      </el-table-column> -->
      <el-table-column width="413px" align="center" label="修改时间" class-name="small-padding fixed-width">
        <template slot-scope="scope">
          <span>{{scope.row.updatetime | parseTime('{y}-{m}-{d}')}}</span>
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
        <el-form-item label="历史添加人" prop="userName">
          <el-input v-model="temp.userName"></el-input>
        </el-form-item>
        <el-form-item label="意见" prop="remake">
          <el-input v-model="temp.remake"></el-input>
        </el-form-item>
        <!-- <el-form-item label="按钮标签" prop="label">
          <el-input v-model="temp.label"></el-input>
        </el-form-item>-
        <el-form-item label="按钮图标" prop="icon">
          <el-input v-model="temp.icon"></el-input>
        </el-form-item>
        <el-form-item label="排  序">
          <el-input-number v-model="temp.sort" :min="1" :max="10000" label=""></el-input-number>
        </el-form-item>
        <el-form-item label="按钮说明">
          <el-input type="textarea" :autosize="{ minRows: 4, maxRows: 6}" placeholder v-model="temp.explain"></el-input>
        </el-form-item> -->
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取消</el-button>
        <el-button v-if="dialogStatus==='create'" type="primary" @click="createData">添加</el-button>
        <el-button v-else-if="dialogStatus==='edit'" type="primary" @click="editData">修改</el-button>
        <el-button v-else type="primary" @click="dialogFormVisible = false">确定</el-button>
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
import waves from "@/directive/waves"; // 水波纹指令
import { parseTime } from "@/utils";
import {historyList, historyAdd, histroyEdit, historyDelete} from "@/api/system/history";
import {menuNoCascadeList} from "@/api/system/menus"

export default{
    name:'HistoryListTable',
    directives: {
        waves
    },
    data(){
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
            PrimaryKey: "updatetime",
            Order: "desc"
        },
        rowSelect: [],
        primaryKeyOptions: [
            { label: "通过Id排序", key: "Id" },
           
            { label: "通过修改时间排序", key: "updateTime" }
        ],
        orderOptions: [
            { label: "正序", key: "asc" },
            { label: "倒序", key: "desc" }
        ],
        buttons: [],
        temp: {
            id: undefined,
            userName: '',
            Remake:'',
            updateTime: new Date(),
          
        },
        dialogFormVisible: false,//添加修改查看层是否显示
        dialogStatus: "",
        textMap: {edit: "修改",create: "添加",see: "查看"},
        dialogPvVisible: false,
        pvData: [],
        dialogConfirm:false,
        confirmData: {title:"",content:"确定要删除这条数据？"},
        rules: {
               
        },
        downloadLoading: false,
        };
    },
    created() {
        this.getHistoryList()
        this.getButttons()
    },
    methods: {
        //列表数据
        getHistoryList() {
            this.listLoading = true;
            historyList(this.listQuery).then(response => {
              console.log(11111);
              console.log(response.data.data);
              console.log(22222);
             
                this.total = response.data.total
                this.list = response.data.data
           
                this.listLoading = false
            })
        },
        getButttons(){
         this.buttons = [
                {id: 1,name: "添加",code: "add",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
                {id: 2,name: "删除",code: "delete",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
                {id: 3,name: "修改",code: "edit",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
                {id: 4,name: "查看",code: "see",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
                // {id: 5,name: "设置流程",code: "setWorkflow",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
                // {id: 6,name: "全部展开",code: "expandall",icon: "el-icon-edit",explain: "",createTime: "",updateTime: "",sort: 1000},
                // {id: 7,name: "全部收缩",code: "collapseall",icon: "el-icon-edit",explain: "", createTime: "",updateTime: "",sort: 1000}
              ];
        },
        //选中行事件
        handleRowChange(currentRow, oldCurrentRow) {
            this.rowSelect = currentRow
        },
        handleFilter() {
            this.listQuery.PageIndex = 1
            this.getButttons()
        },
        handleSizeChange(val) {
            this.listQuery.PageSize = val
            this.getButttons()
        },
        handleCurrentChange(val) {
            this.listQuery.PageIndex = val
            this.getButttons()
        },
        //重置单条数据
        resetTemp() {
            this.temp = {
                id: undefined,
                userName: '',
                remake: '',
             
                updateTime: new Date()
             
            }
        },
        //按钮通用事件
        handleElement(code) {
          code = code.toLocaleLowerCase()
          if (code === "add") {
              this.handleCreate()
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
        handleCreate() {
            this.resetTemp()
            this.dialogStatus = 'create'
            this.dialogFormVisible = true
            this.$nextTick(() => {
                this.$refs['dataForm'].clearValidate()
            })
        },
        createData() {
            this.$refs["dataForm"].validate(valid => {
            if (valid) {
                const tempData = Object.assign({}, this.temp)
                historyAdd(tempData)
                    .then(response => {
                    this.dialogFormVisible = false
                    if (response.data.code === 200) {
                        this.$notify({title: "成功",message: response.data.message,type: "success",duration: 2000})
                        //重新加载列表
                        this.getButttons()
                        this.getHistoryList()
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
                historyDelete(_ids)
                .then(response => {
                    if (response.data.code === 200) {
                        this.$notify({title: "成功",message: "删除成功",type: "success",duration: 2000})
                        //重新加载列表
                        this.getButttons()

                    }
                    else
                    {
                        this.$notify({title: "失败",message: "删除失败！",type: "error",duration: 2000})
                    }
                })
                .catch(e => {
                    this.$notify({title: "失败",message: "删除失败！",type: "error",duration: 2000})
                });
            }else{
                this.$notify({title: "失败",message: "基本按钮无法删除！",type: "error",duration: 2000})
            }
        },
            //修改
        handleEdit() {
            if (this.rowSelect.length > 1) {
                this.$notify({title: "失败",message: "不支持多条修改",type: "error",duration: 2000})
                this.$refs.HistoryListTable.clearSelection();
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
                    histroyEdit(tempData)
                        .then(response => {
                        if (response.data.code === 200){
                            this.$notify({title: "成功",message: response.data.message,type: "success",duration: 2000})
                            this.$refs.HistoryListTable.clearSelection()
                            this.dialogFormVisible = false
                            this.getButttons()
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
                this.$refs.HistoryListTable.clearSelection()
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











