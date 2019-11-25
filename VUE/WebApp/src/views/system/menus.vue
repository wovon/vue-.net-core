<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button
        class="filter-item"
        v-for="b in buttons"
        @click="handleElement(b.code)"
        :key="b.Id"
        :icon="b.Icon"
        type="primary"
      >{{b.name}}</el-button>
    </div>
    <tree-table
      :data="menuData"
      ref="menuTable"
      :firstWidth="firstWidth"
      :evalFunc="func"
      @func="getRowSelect"
      :evalArgs="args"
      :expandAll="expandAll"
      border
    >
      <el-table-column label="标识码">
        <template slot-scope="scope">
          <span>{{scope.row.code}}</span>
        </template>
      </el-table-column>
      <el-table-column label="图  标" width="60" min-width="60px" align="center">
        <template slot-scope="scope">
          <span>{{scope.row.icon}}</span>
        </template>
      </el-table-column>
      <el-table-column label="Path">
        <template slot-scope="scope">
          <span>{{scope.row.path}}</span>
        </template>
      </el-table-column>
      <el-table-column label="Component">
        <template slot-scope="scope">
          <span>{{scope.row.component}}</span>
        </template>
      </el-table-column>
      <el-table-column label="Redirect">
        <template slot-scope="scope">
          <span>{{scope.row.redirect}}</span>
        </template>
      </el-table-column>
      <el-table-column label="linkAddress">
        <template slot-scope="scope">
          <span>{{scope.row.linkAddress}}</span>
        </template>
      </el-table-column>
      <el-table-column label="排序" width="60" min-width="60px" align="center">
        <template slot-scope="scope">
          <span>{{scope.row.sort}}</span>
        </template>
      </el-table-column>
      <el-table-column label="对应按钮">
        <template slot-scope="scope">
          <span style="color:red;">{{scope.row.buttonNames}}</span>
        </template>
      </el-table-column>
      <el-table-column label="隐藏" width="50" min-width="50px" align="center">
        <template slot-scope="scope">
          <el-checkbox v-model="scope.row.hidden"></el-checkbox>
        </template>
      </el-table-column>
      <el-table-column label="添加时间">
        <template slot-scope="scope">
          <span>{{scope.row.createTime | parseTime('{y}-{m}-{d}')}}</span>
        </template>
      </el-table-column>
    </tree-table>

    <!-- 弹出层（添加，修改，查看） -->
    <el-dialog :title="dialogMap[dialogStatus]" :visible.sync="dialogFromVisible">
      <el-form
        ref="menuFrom"
        :model="temp"
        label-position="right"
        label-width="80px"
        style="width: 400px; margin-left:100px;"
      >
        <el-form-item label="名  称">
          <el-input v-model="temp.name" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="上一级">
          <treeselect
            :multiple="true"
            :options="menuData"
            :disable-branch-nodes="false"
            v-model="temp.parentId"
            :value="temp.parentId"
            :flat="true"
            :defaultExpandLevel="Infinity"
            :maxHeight="200"
            search-nested
            placeholder
            style="width:450px;"
          />
        </el-form-item>
        <el-form-item label="标识码">
          <el-input v-model="temp.code" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="图  标">
          <el-input v-model="temp.icon" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="Path">
          <el-input v-model="temp.path" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="Component">
          <el-input v-model="temp.component" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="Redirect">
          <el-input v-model="temp.redirect" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="LinkAddress">
          <el-input v-model="temp.linkAddress" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="是否隐藏">
          <el-checkbox v-model="temp.hidden"></el-checkbox>
        </el-form-item>
        <!-- <el-form-item label="设置对应子类的ID" >
          <treeselect :options="treeData" v-model="temp.useChildId" :disable-branch-nodes="false" :show-count="false" placeholder="" :flat="true" :defaultExpandLevel="Infinity" :maxHeight="200" style="width:450px;" />
        </el-form-item>-->
        <el-form-item label="排  序">
          <el-input-number v-model="temp.sort" :min="1" :max="10000" label></el-input-number>
        </el-form-item>
        <!-- <el-form-item label="说  明">
          <el-input type="textarea" :autosize="{ minRows: 4, maxRows: 8}" placeholder="请输入内容" v-model="temp.explain" style="width:450px">
          </el-input>
        </el-form-item>-->
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFromVisible=false">取消</el-button>
        <el-button v-if="dialogStatus=='add'" @click="handleAdd" type="primary">添加</el-button>
        <el-button v-if="dialogStatus=='edit'" @click="handleEidt" type="primary">修改</el-button>
        <el-button v-if="dialogStatus=='see'" @click="dialogFromVisible=false" type="primary">确认</el-button>
      </div>
    </el-dialog>
    <!-- 弹出层（设置按钮） -->
    <el-dialog title="设置按钮" :visible.sync="dialogSetMenuVisible">
      <el-form
        ref="menuFrom"
        :model="temp"
        label-position="left"
        label-width="80px"
        style="width: 400px; margin-left:100px;"
      >
        <el-form-item label="名称">
          <el-input v-model="setMenuButtonTemp.name" style="width:450px;"></el-input>
        </el-form-item>
        <el-form-item label="按钮">
          <treeselect
            :options="buttonData"
            v-model="setMenuButtonTemp.buttons"
            :value="setMenuButtonTemp.buttons"
            :multiple="true"
            :disable-branch-nodes="false"
            :show-count="false"
            placeholder
            :flat="true"
            :defaultExpandLevel="Infinity"
            :maxHeight="200"
            style="width:450px;"
          />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogSetMenuVisible=false">取消</el-button>
        <el-button @click="handleSetDepartment();dialogSetMenuVisible=false" type="primary">确认</el-button>
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
  Created: 2019-01-23 14:54
*/
import treeTable from "@/components/TreeTable";
import treeToArray from "@/views/example/table/treeTable/customEval";
import { deptList } from "@/api/system/departments";
import {
  menuList,
  menuNoCascadeList,
  menuAuthorityList,
  menuAdd,
  menuDelete,
  menuEdit,
  setMenuButton
} from "@/api/system/menus";
import {
  buttonList,
  buttonListByMenuIds,
  buttonListByMenuName
} from "@/api/system/buttons";
import { parseTime } from "@/utils";

// 下拉树
import Treeselect from "@riophae/vue-treeselect";
import "@riophae/vue-treeselect/dist/vue-treeselect.css";

export default {
  name: "customMenuTable",
  components: { treeTable, Treeselect },
  data() {
    return {
      firstWidth: "300px", //名称列的宽度
      func: treeToArray,
      expandAll: true,
      treeData: [],
      buttons: [],
      buttonData: [],
      menuData: [],
      args: [null, null, ""],
      rowSelect: [],
      dialogSetMenuVisible: false,
      dialogConfirm: false,
      confirmData: { title: "", content: "确定要删除这条数据？" },
      dialogFromVisible: false,
      dialogStatus: "",
      dialogMap: {
        add: "添加",
        delete: "删除",
        edit: "修改",
        see: "查看",
        setButton: "设置按钮"
      },
      temp: {
        id: "",
        name: "",
        label: "",
        parentId: [],
        parentName: "",
        cascadeId: "",
        code: "",
        linkAddress: "",
        icon: "",
        title: "",
        path: "",
        redirect: "",
        component: "",
        hidden: false,
        createTime: new Date(),
        updateTime: new Date(),
        isAble: false,
        sort: 1000
      },
      setMenuButtonTemp: {
        name: "",
        menus: [],
        buttons: [],
        data: []
      }
    };
  },
  created() {
    this.getButttons();
    this.getMenuList();
    this.getButtonList();
  },
  methods: {
    //获取操作按钮
    getButttons() {
      let _Queery = {
        name: this.$route.name,
        component: this.$route.path.substr(1)
      };
      buttonListByMenuName(_Queery).then(response => {
        this.buttons = response.data.data;
      });
    },
    message(row) {
      this.$message.info(row.event);
    },
    getRowSelect(rows) {
      this.rowSelect = rows;
    },
    //读取部门下拉列表
    getButtonList() {
      buttonList()
        .then(response => {
          //this.treeData = response.data.data;
          this.buttonData = response.data.data;
        })
        .catch(e => {
          //console.log(e)
        });
    },
    //菜单列表
    getMenuList() {
      menuList()
        .then(response => {
          //console.log('json', JSON.stringify(response.data.data))
          //console.log(response.data.data);
          this.menuData = response.data.data;
        })
        .catch(e => {});
    },
    //部门对应的角色
    async getMenuButtonList(s) {
      await buttonListByMenuIds(s)
        .then(response => {
          //console.log(s);
          //console.log(response);
          var ids = [];
          response.data.data.forEach(row => {
            ids.push(row.id);
          });
          this.setMenuButtonTemp.buttons = ids;
          this.dialogSetMenuVisible = true;
        })
        .catch(e => {});
    },
    clearMenuTemp() {
      this.setMenuButtonTemp = {
        name: "",
        menus: [],
        buttons: [],
        data: []
      };
    },
    handleElement(code) {
      code = code.toLocaleLowerCase();
      this.dialogStatus = code;
      if (code === "expandall") {
        this.expandAll = true;
      } else if (code === "collapseall") {
        this.expandAll = false;
      }
      //添加
      else if (code === "add") {
        this.dialogFromVisible = true;
      }
      //删除
      else if (code === "delete") {
        if (this.rowSelect.length == 0) {
          this.$notify({
            title: "提示",
            message: "请选择要删除的数据",
            type: "info",
            duration: 2000
          });
        } else if (this.rowSelect.length > 0) {
          this.dialogConfirm = true;
          this.confirmData.content =
            "你确定要删除这" +
            this.rowSelect.length +
            '条数据？删除之后相关联的下级也将删除。<font color="red">如有共用下级情况，对应下级也将删除。请谨慎操作。</font>';
        }
      }
      //修改
      else if (code === "edit" || code === "see") {
        if (this.rowSelect.length === 0) {
          this.$notify({
            title: "提示",
            message: "请选择要" + this.dialogMap[this.dialogStatus] + "的数据",
            type: "info",
            duration: 2000
          });
        } else if (this.rowSelect.length > 1) {
          this.$notify({
            title: "提示",
            message: "不支持多条数据" + this.dialogMap[this.dialogStatus],
            type: "info",
            duration: 2000
          });
          this.$refs.menuTable.handleClearSelection();
        } else {
          this.temp = Object.assign({}, this.rowSelect[0]);
          delete this.temp.parent;
          delete this.temp.children;
          this.temp.parentId =
            this.temp.parentId != undefined
              ? this.temp.parentId.split(",")
              : [];
          this.dialogFromVisible = true;
          this.$nextTick(() => {
            this.$refs["menuFrom"].clearValidate();
          });
        }
      }
      //设置按钮
      else if (code == "setbutton") {
        if (this.rowSelect.length === 0) {
          this.$notify({
            title: "提示",
            message: "请选择要" + this.dialogMap[this.dialogStatus] + "的数据",
            type: "info",
            duration: 2000
          });
        } else {
          var ids = "";
          this.clearMenuTemp();
          this.rowSelect.forEach((row, index) => {
            this.setMenuButtonTemp.name += row.name + ",";
            this.setMenuButtonTemp.menus.push(row.id);
            ids += row.id + ",";
          });
          this.setMenuButtonTemp.name = this.setMenuButtonTemp.name.replace(
            /,$/gi,
            ""
          );
          this.getMenuButtonList({ menuIds: ids });
          //this.dialogSetRoleVisible = true
        }
      }
    },
    handleAdd() {
      const tempData = Object.assign({}, this.temp);
      tempData.parentId =
        tempData.parentId != undefined ? tempData.parentId.join(",") : "";

      this.$refs["menuFrom"].validate(valid => {
        menuAdd(tempData)
          .then(response => {
            if (response.data.code === 200) {
              this.$notify({
                title: "成功",
                message: "添加成功！",
                type: "success",
                duration: 2000
              });
              this.dialogFromVisible = false;
              this.getMenuList();
            } else {
              this.$notify({
                title: "失败",
                message: "添加失败！",
                type: "error",
                duration: 2000
              });
            }
          })
          .catch(e => {
            this.$notify({
              title: "失败",
              message: "添加失败！catch",
              type: "error",
              duration: 2000
            });
          });
      });
    },
    handleDelete() {
      let _ids = { ids: "" };
      this.rowSelect.forEach((row, index) => {
        _ids.ids += row.id + ",";
      });
      if (_ids.ids != "") {
        menuDelete(_ids)
          .then(response => {
            if (response.data.code === 200) {
              this.$notify({
                title: "成功",
                message: this.dialogMap[this.dialogStatus] + "成功！",
                type: "success",
                duration: 2000
              });
              this.getMenuList();
            } else {
              this.$notify({
                title: "失败",
                message: this.dialogMap[this.dialogStatus] + "失败！",
                type: "error",
                duration: 2000
              });
            }
          })
          .catch(e => {
            this.$notify({
              title: "失败",
              message: this.dialogMap[this.dialogStatus] + "失败！catch",
              type: "error",
              duration: 2000
            });
          });
      }
    },
    handleEidt() {
      const tempData = Object.assign({}, this.temp);
      tempData.parentId =
        tempData.parentId != undefined ? tempData.parentId.join(",") : "";
      this.$refs["menuFrom"].validate(valid => {
        menuEdit(tempData)
          .then(response => {
            if (response.data.code === 200) {
              this.dialogFromVisible = false;
              this.$notify({
                title: "成功",
                message: "修改成功",
                type: "success",
                duration: 2000
              });
              this.getMenuList();
            } else {
              this.$notify({
                title: "失败",
                message: "修改失败",
                type: "error",
                duration: 2000
              });
            }
          })
          .catch(e => {
            this.$notify({
              title: "失败",
              message: "修改失败！catch",
              type: "error",
              duration: 2000
            });
          });
      });
    },
    //设置角色
    handleSetDepartment() {
      this.setMenuButtonTemp.menus.forEach((row, index) => {
        this.setMenuButtonTemp.buttons.forEach(row2 => {
          this.setMenuButtonTemp.data.push({ menuId: row, buttonId: row2 });
        });
        if (this.setMenuButtonTemp.data.length == 0) {
          this.setMenuButtonTemp.data.push({ menuid: "", buttonId: row });
        }
      });
      //console.log(this.setMenuButtonTemp.data)
      setMenuButton({ menuButtons: this.setMenuButtonTemp.data })
        .then(response => {
          if (response.data.code === 200) {
            this.getMenuList();
            this.dialogSetMenuVisible = false;
            this.$notify({
              title: "成功",
              message: "设置成功",
              type: "success",
              duration: 2000
            });
            this.$refs.menuTable.handleClearSelection();
          } else {
            this.$notify({
              title: "失败",
              message: "设置失败",
              type: "error",
              duration: 2000
            });
          }
        })
        .catch(e => {
          this.$notify({
            title: "失败",
            message: "设置失败！catch",
            type: "error",
            duration: 2000
          });
        });
    }
  }
};
</script>
