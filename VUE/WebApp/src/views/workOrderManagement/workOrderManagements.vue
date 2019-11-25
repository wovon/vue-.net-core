<template>
  <div class="app-container calendar-list-container">
    <div class="filter-container">
      <!-- <el-input @keyup.enter.native="handleFilter" style="width: 200px;" class="filter-item" placeholder="关键词" v-model="listQuery.KeyWorld"></el-input> -->
      <el-select @change="handleFilter" style="width: 160px" class="filter-item" v-model="listQuery.PrimaryKey" >
        <el-option v-for="item in primaryKeyOptions" :key="item.key" :label="item.label" :value="item.key" ></el-option>
      </el-select>
      <el-select @change="handleFilter" style="width: 140px" class="filter-item" v-model="listQuery.Order" >
        <el-option v-for="item in orderOptions" :key="item.key"  :label="item.label" :value="item.key" ></el-option>
      </el-select>
      <!-- <el-button class="filter-item" type="primary" style="margin-left:3px" v-waves icon="el-icon-search" @click="handleFilter">筛选</el-button> -->
    </div>
    <div class="filter-container">
      <el-button class="filter-item" v-for="b in buttons" @click="handleElement(b.code)" :key="b.Id" :icon="b.Icon" type="primary" >{{b.name}}</el-button>
    </div>
    <el-table :key="tableKey"
      highlight-row
      ref="modifiersListTable"
      :data="list"
      v-loading="listLoading"
      @on-current-change="handleRowChange"
      @selection-change="handleRowChange"
      element-loading-text="给我一点时间"
      border
      fit
      highlight-current-row
      style="width: 100%"
    >
      <el-table-column type="selection" width="55"></el-table-column>
      <!-- <el-table-column align="center" label="序号" width="100px">
        <template slot-scope="scope">
          <span>{{scope.row.id}}</span>
        </template>
      </el-table-column>-->
      <el-table-column width="100px" align="center" label="标题">
        <template slot-scope="scope">
          <span>{{scope.row.title}}</span>
        </template>
      </el-table-column>
      <el-table-column width="200px" align="center" label="反馈时间" class-name="small-padding fixed-width">
        <template slot-scope="scope">
          <span>{{scope.row.feedbackTime | parseTime('{y}-{m}-{d}')}}</span>
        </template>
      </el-table-column>
      <el-table-column width="100px" align="center" label="反馈给">
        <template slot-scope="scope">
          <span>{{scope.row.feedbackUserName}}</span>
        </template>
      </el-table-column>
      <el-table-column width="100px" align="center" label="优先级">
        <template slot-scope="scope">
          <span>{{scope.row.priority}}</span>
        </template>
      </el-table-column>
      <el-table-column width="100px" align="center" label="类别">
        <template slot-scope="scope">
          <span>{{scope.row.type}}</span>
        </template>
      </el-table-column>
      <el-table-column width="100px" align="center" label="排序">
        <template slot-scope="scope">
          <span>{{scope.row.sort}}</span>
        </template>
      </el-table-column>

      <el-table-column width="200px" align="center" label="添加时间" class-name="small-padding fixed-width">
        <template slot-scope="scope">
          <span>{{scope.row.createTime | parseTime('{y}-{m}-{d}')}}</span>
        </template>
      </el-table-column>
      <el-table-column width="200px" align="center" label="修改时间" class-name="small-padding fixed-width">
        <template slot-scope="scope">
          <span>{{scope.row.updateTime | parseTime('{y}-{m}-{d}')}}</span>
        </template>
      </el-table-column>
    </el-table>
    <!-- 分页 -->
    <div class="pagination-container">
      <el-pagination
        background
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        :current-page="listQuery.PageIndex"
        :page-sizes="[2,5,10,20,30, 50]"
        :page-size="listQuery.PageSize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="total"
      ></el-pagination>
    </div>
    <!-- 添加 修改 查看层 -->
    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible" style="height:100%;">
      <el-form :rules="rules" ref="dataForm" :model="temp" label-position="center" label-width="80px" style="width: 90%; margin-left:1%;height:90%;" >
        <el-row>
          <el-col :span="12">
            <el-form-item label="项目" prop="Projects">
              <el-input v-model="temp.projects"></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="流程" prop="wrokFlows">
              <el-input v-model="temp.wrokFlows"></el-input>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="12">
            <el-form-item label="标题" prop="title">
              <el-input v-model="temp.title"></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="上个问题" prop="parentId">
              <el-input v-model="temp.parentId"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="问题详细" prop="content">
              <el-input v-model="temp.content"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">

          </el-col>
          <el-col :span="12">
            
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">

          </el-col>
          <el-col :span="12">
            
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">

          </el-col>
          <el-col :span="12">
            
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">

          </el-col>
          <el-col :span="12">
            
          </el-col>
        </el-row>


        <!-- 
        
        

        <el-form-item label="反馈时间" prop="feedbackTime">
          <el-input v-model="temp.feedbackTime"></el-input>
        </el-form-item>
        <el-form-item label="反馈给用户ID" prop="feedbackUserId">
          <el-input v-model="temp.feedbackUserId"></el-input>
        </el-form-item>
        <el-form-item label="反馈给谁" prop="feedbackUserName">
          <el-input v-model="temp.feedbackUserName"></el-input>
        </el-form-item>
        <el-form-item label="优先级" prop="priority">
          <el-input v-model="temp.priority"></el-input>
        </el-form-item>
        <el-form-item label="指定修改人" prop="modifiers">
          <el-input v-model="temp.modifiers"></el-input>
        </el-form-item>
        <el-form-item label="修改人记录" prop="histories">
          <el-input v-model="temp.histories"></el-input>
        </el-form-item>
        <el-form-item label="修改状态" prop="selectOptions">
          <el-input v-model="temp.selectOptions"></el-input>
        </el-form-item>

        <el-form-item label="用户ID" prop="userId">
          <el-input v-model="temp.userId"></el-input>
        </el-form-item>
        <el-form-item label="用户名" prop="userName">
          <el-input v-model="temp.userName"></el-input>
        </el-form-item>
        <el-form-item label="部门ID" prop="departmentId">
          <el-input v-model="temp.departmentId"></el-input>
        </el-form-item>
        <el-form-item label="部门名" prop="departmentName">
          <el-input v-model="temp.departmentName"></el-input>
        </el-form-item>
        <el-form-item label="类别" prop="type">
          <el-input v-model="temp.type"></el-input>
        </el-form-item>
        <el-form-item label="排序" prop="sort">
          <el-input v-model="temp.sort"></el-input>
        </el-form-item>


        <el-form-item label="备注">
          <el-input type="textarea" :autosize="{ minRows: 4, maxRows: 6}" placeholder v-model="temp.explain" ></el-input>
        </el-form-item> -->
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取消</el-button>
        <el-button v-if="dialogStatus==='create'" type="primary" @click="createData">添加</el-button>
        <el-button v-else-if="dialogStatus==='edit'" type="primary" @click="editData">修改</el-button>
        <el-button v-else type="primary" @click="dialogFormVisible = false">确定</el-button>
      </div>
    </el-dialog>
    <!--
     删除提示
    <el-dialog title="提示" :visible.sync="dialogConfirm" style="width:666px;margin:0 auto;">
      <span>{{confirmData.content}}</span>
      <span slot="footer" class="dialog-footer">
        <el-button @click="dialogConfirm = false">取消</el-button>
        <el-button type="primary" @click="handleDelete();dialogConfirm = false">确定</el-button>
      </span>
    </el-dialog>-->
  </div>
</template>

<script>
  import waves from "@/directive/waves"; // 水波纹指令
  import { parseTime } from "@/utils";
  import {buttonListByMenuName} from "@/api/system/buttons";//通过路由名获取对应按钮
  import {WorkOrderManagementsList,WorkOrderManagementsAdd,WorkOrderManagementsDelete,WorkOrderManagementsEdit} from "@/api/workOrderManagement/workOrderManagements";//通过路由名获取对应按钮
  export default{
    name:"workOrderManagementsTable",
    directives: {
      waves
    },
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
        buttons: [],
        temp: {
          id: undefined,
          name: "",
          projects:[],//项目
          workFlows:[],//流程
          title: "",
          content: "",
          parentId: "",
          feedbackTime: new Date(),
          feedbackUserId:"",
          feedbackUserName:"",
          priority:undefined,//优先级
          modifiers:[],//指定修改人
          histories:[],
          selectOptions:[],//修改状态
          userId:"",
          userName:"",
          departmentId:"",
          departmentName:"",
          type:undefined,
          origin:"",//来源
          createTime: new Date(),
          updateTime: new Date(),
          sort:1000,
          isDel:false
        },
        dialogFormVisible: false, //添加，修改，查看层是否显示
        dialogStatus: "",
        textMap: { edit: "修改", create: "添加", see: "查看" },
        dialogConfirm: false,
        confirmData: { title: "", content: "确定要删除这条数据？" },
        rules: {
          // name: [{ required: true, message: '请填写名称', trigger: 'blur' }],
          // code: [{ required: true, message: '请填写编号', trigger: 'blur' }],
          // icon: [{ required: true, message: '请填写图标', trigger: 'blur' }],
          // explain: [{ required: true, message: '请填写说明', trigger: 'blur' }]
        },
        downloadLoading: false
      }
    },
    created() {
      this.getButttons();
      this.getWorkOrderManagementsList();
    },
    methods: {
      //列表数据
      getWorkOrderManagementsList() {
        this.listLoading = true;
        WorkOrderManagementsList(this.listQuery).then(response => {
          this.list = response.data.items
          this.total = response.data.total
          this.listLoading = false
        });
      },
      getButttons() {
        let _Queery = {
          name: this.$route.name,
          component: this.$route.path.substr(1)
        };
        buttonListByMenuName(_Queery).then(response => {
          this.buttons = response.data.data;
        });
      },
      //选中行事件
      handleRowChange(currentRow, oldCurrentRow) {
        this.rowSelect = currentRow;
      },
      handleFilter() {
        this.listQuery.PageIndex = 1;
        this.getModigerList();
      },
      handleSizeChange(val) {
        this.listQuery.PageSize = val;
        this.getModigerList();
      },
      handleCurrentChange(val) {
        this.listQuery.PageIndex = val;
        this.getModigerList();
      },
      //重置单条数据
      resetTemp() {
        this.temp = {
          id: undefined,
          woM_Id: "",
          updateNumber: 0,
          sO_Id: "",
          user_Id: "",
          userName: "",
          createTime: new Date(),
          updateTime: new Date()
        };
      },
      //通用事件
      handleElement(code) {
        code = code.toLocaleLowerCase();
        if (code === "add") {
          this.resetTemp()
          this.dialogStatus = 'add'
          this.dialogFormVisible = true
          this.$nextTick(() => {
              this.$refs['dataForm'].clearValidate()
          })
        } else if (code === "delete") {
          if (this.rowSelect.length > 0) {
            this.dialogConfirm = true;
            this.confirmData.content = "你确定要删除这" + this.rowSelect.length + "条数据？";
          } else {
            this.$notify({title: "提示",message: "请选择数据！",type: "info",duration: 2000});
          }
        }
        //修改
        else if (code === "edit") {
          //this.handleEdit();
        }
        //查看
        else if (code === "see") {
          //this.handleSee();
        }
      },
    }
  }
</script>
