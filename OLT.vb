Public Class OLT

    Dim StartVLAN As String
    Dim 命令行 As String
    Dim SLOT As String
    Dim SLOTType As String
    Dim outcmd As String
    Dim 保存配置 As String
    Dim 宽带SVLAN As Integer
    Dim 语音SLVLAN As Integer
    Dim 宽带SVLAN2 As Integer
    Dim 命令1 As String
    Dim 命令2 As String
    Dim 命令3 As String
    Dim 宽带起始VLAN As String
    Dim 宽带终止VLAN As String
    Dim ip地址 As String
    Dim 月份 As String

    Private Sub RadioButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton1.Click
        Me.扩容起始VLAN2.Visible = False
        Me.Label6.Visible = False
        Me.Label5.Text = "宽带起始VLAN"
    End Sub
    Private Sub RadioButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton2.Click
        Me.扩容起始VLAN2.Visible = True
        Me.Label6.Visible = True
        Me.Label5.Text = "语音起始SVLAN"
    End Sub
    Private Sub 扩容拷贝代码_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 扩容拷贝代码.Click
        If 扩容代码.Text.Trim() = "" Then
            MessageBox.Show("代码为空，请生成代码后复制！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Clipboard.SetText(扩容代码.Text)
            MessageBox.Show("代码已经复制！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
    End Sub
    Private Sub 扩容生成代码_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 扩容生成代码.Click
        SLOT = Int(Me.槽位.Text)
        SLOTType = Me.板卡型号.Text
        If RadioButton1.Checked = True Then
            If 扩容起始VLAN1.Text.Length = 4 And 扩容起始VLAN1.Text.Trim() <> "" Then
                StartVLAN = Int(Me.扩容起始VLAN1.Text)
                If 板卡型号.Text.Length = 4 And 板卡型号.Text.Trim() <> "" Then
                    If SLOT >= 2 And SLOT <= 17 Then
                        Call 扩容1()
                        Me.扩容代码.Text = outcmd & 命令3 & "exit" & vbCrLf & vbCrLf & "write" & vbCrLf
                        命令1 = ""
                        命令2 = ""
                        命令3 = ""
                    Else
                        MessageBox.Show("槽位设定不在范围内，请重新输入！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("板卡型号输入不准确，请重新输入！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            Else
                MessageBox.Show("VLAN填写不符合规范，请重新输入！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            ElseIf RadioButton2.Checked = True Then
                If 扩容起始VLAN1.Text.Trim() <> "" And 扩容起始VLAN2.Text.Trim() <> "" And Me.扩容起始VLAN2.Text.Length = 4 And Me.扩容起始VLAN1.Text.Length = 4 Then
                StartVLAN = Int(Me.扩容起始VLAN1.Text)
                If 板卡型号.Text.Length = 4 And 板卡型号.Text.Trim() <> "" Then
                    If SLOT >= 2 And SLOT <= 17 Then
                        Call 扩容2()
                        Me.扩容代码.Text = outcmd & 命令3 & "exit" & vbCrLf & vbCrLf & "write" & vbCrLf
                        命令1 = ""
                        命令2 = ""
                        命令3 = ""
                    Else
                        MessageBox.Show("槽位设定不在范围内，请重新输入！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("板卡型号输入不准确，请重新输入！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            Else
                MessageBox.Show("VLAN填写不符合规范，请重新输入！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            End If
    End Sub
    Private Sub 新建生成代码_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新建生成代码.Click
        Call chk_ip()
        If Hostname.Text.Length = 16 And Hostname.Text.Trim() <> "" Then
            If IsNumeric(C300新建起始VLAN.Text) And IsNumeric(C300新建终止VLAN.Text) Then
                If C300新建起始VLAN.Text.Length = 4 And C300新建终止VLAN.Text.Length = 4 And C300新建起始VLAN.Text.Trim() <> "" And C300新建终止VLAN.Text.Trim() <> "" Then
                    If CInt(C300新建起始VLAN.Text) < CInt(C300新建终止VLAN.Text) Then
                        If chk_ip() = 0 Then
                            Call 新建()
                            Me.新建代码.Text = 命令行
                        ElseIf chk_ip() = 1 Then
                            MessageBox.Show("请输入正确的IP地址！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        ElseIf chk_ip() = 2 Then
                            MessageBox.Show("IP地址只能输入数字！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        ElseIf chk_ip() = 3 Then
                            MessageBox.Show("请输入正确的IP地址范围（0-255）！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        ElseIf chk_ip() = 4 Then
                            MessageBox.Show("设备管理IP地址最后一位不可0 ！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("起始VLAN必须小于终止VLAN,请重新输入!", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("请输入正确的起始和终止VLAN！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            Else
                MessageBox.Show("VLAN必须为数字，请重新输入！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            Else
                MessageBox.Show("Hostname填写不符合规范，请重新输入！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
    End Sub
    Private Sub 新建拷贝代码_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新建拷贝代码.Click
        If 新建代码.Text.Trim() = "" Then
            MessageBox.Show("代码为空，请生成代码后复制！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Clipboard.SetText(新建代码.Text)
            MessageBox.Show("代码已经复制！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
    End Sub
    Function 扩容1()
        Dim i As Integer
        Dim Q As Integer
        Dim Z As Integer
        outcmd = "con t" & vbCrLf &
                 "add-card slot " & SLOT & " " & SLOTType & vbCrLf &
                 "vlan-smart-qinq enable" & vbCrLf & "epon" & vbCrLf &
                 "onu-authentication-mode service 1/" & SLOT & " hybrid unknown-onu-reject disable" & vbCrLf &
                 "y" & vbCrLf +
                 "!" & vbCrLf
        For i = 1 To 8 Step 1
            命令1 = "interface epon-olt_1/" & SLOT & "/" & i & vbCrLf &
                      "vlan-smart-qinq enable" & vbCrLf &
                      "no shutdown" & vbCrLf &
                      "!" & vbCrLf
            outcmd = outcmd & 命令1 & vbCrLf
        Next
        For Z = 1 To 8 Step 1
            Q = StartVLAN + (Z - 1)
            命令2 = "vlan-smart-qinq ingress-port epon-olt_1/" & SLOT & "/" & Z & " cvlan 1001 to 1300 svlan " & Q & vbCrLf &
                      "vlan-smart-qinq ingress-port epon-olt_1/" & SLOT & "/" & Z & " cvlan 6 to 20 svlan " & Q & vbCrLf &
                      "vlan-smart-qinq ingress-port epon-olt_1/" & SLOT & "/" & Z & " cvlan 31 to 33 svlan " & Q & vbCrLf
            命令3 = 命令3 & 命令2 & vbCrLf
        Next
    End Function
    Function 扩容2()
        Dim i As Integer
        Dim Q As Integer
        Dim Z As Integer
        语音SLVLAN = Me.扩容起始VLAN1.Text
        宽带SVLAN2 = Me.扩容起始VLAN2.Text
        outcmd = "con t" & vbCrLf &
                 "add-card slot " & SLOT & " " & SLOTType & vbCrLf &
                 "vlan-smart-qinq enable" & vbCrLf & "epon" & vbCrLf &
                 "onu-authentication-mode service 1/" & SLOT & " hybrid unknown-onu-reject disable" & vbCrLf &
                 "y" & vbCrLf +
                 "!" & vbCrLf
        For i = 1 To 8 Step 1
            命令1 = "interface epon-olt_1/" & SLOT & "/" & i & vbCrLf &
                      "vlan-smart-qinq enable" & vbCrLf &
                      "no shutdown" & vbCrLf &
                      "!" & vbCrLf
            outcmd = outcmd & 命令1 & vbCrLf
        Next
        For Z = 1 To 8 Step 1
            Q = 语音SLVLAN + (Z - 1)
            命令2 = "vlan-smart-qinq ingress-port epon-olt_1/" & SLOT & "/" & Z & " cvlan 1001 to 1300 svlan " & 宽带SVLAN2 & vbCrLf &
                    "vlan-smart-qinq ingress-port epon-olt_1/" & SLOT & "/" & Z & " cvlan 45 svlan 401 newcos 7" & vbCrLf &
                    "vlan-smart-qinq ingress-port epon-olt_1/" & SLOT & "/" & Z & " cvlan 4050 to 4060 svlan " & Q & " newcos 6" & vbCrLf &
                    "vlan-smart-qinq ingress-port epon-olt_1/" & SLOT & "/" & Z & " cvlan 6 to 20 svlan " & 宽带SVLAN2 & vbCrLf &
                    "vlan-smart-qinq ingress-port epon-olt_1/" & SLOT & "/" & Z & " cvlan 32 to 33 svlan " & 宽带SVLAN2 & vbCrLf
            命令3 = 命令3 & 命令2 & vbCrLf
        Next
    End Function
    Private Sub 扩容重置_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 扩容重置.Click
        槽位.Text = "2"
        板卡型号.Text = "ETTO"
        扩容起始VLAN1.Text = "1001"
        扩容起始VLAN2.Text = "3001"
        扩容代码.Text = ""
        命令1 = ""
        命令2 = ""
        命令3 = ""
    End Sub
    Function 月()
        Dim y As Integer
        y = Date.Now.Month
        Select Case y
            Case 1
                月份 = "Jan"
            Case 2
                月份 = "Feb"
            Case 3
                月份 = "Mar"
            Case 4
                月份 = "Apr"
            Case 5
                月份 = "May"
            Case 6
                月份 = "Jun"
            Case 7
                月份 = "Jul"
            Case 8
                月份 = "Aug"
            Case 9
                月份 = "Sep"
            Case 10
                月份 = "Oct"
            Case 11
                月份 = "Nov"
            Case 12
                月份 = "Dec"
        End Select
    End Function
    Function 新建()
        Dim 时间 As String
        Dim 年 As String
        Dim 日 As String
        Dim 分组ip() As String
        Dim RouteIP As String
        Dim 备用ip地址 As String
        Dim 备用RouteIP As String
        Dim hostname As String
        宽带起始VLAN = C300新建起始VLAN.Text
        宽带终止VLAN = C300新建终止VLAN.Text
        hostname = Me.Hostname.Text
        时间 = DateTime.Now.ToLongTimeString
        日 = Date.Now.Day
        年 = Date.Now.Year
        Call 月()
        ip地址 = Me.C300新建管理IP.Text
        分组ip = Split(ip地址, ".", -1, 1)
        ip地址 = Me.C300新建管理IP.Text
        RouteIP = 分组ip(0) & "." & 分组ip(1) & "." & 分组ip(2) & "." & Int(分组ip(3)) - 1
        备用ip地址 = "1" & 分组ip(0) & "." & 分组ip(1) & "." & 分组ip(2) & "." & 分组ip(3)
        备用RouteIP = "1" & 分组ip(0) & "." & 分组ip(1) & "." & 分组ip(2) & "." & Int(分组ip(3)) - 1
        命令行 = "con t" & vbCrLf & vbCrLf &
                 "username wjzx password 68302234 privilege 15" & vbCrLf &
                 "username szwjzx password Sznoc2018)& privilege 15" & vbCrLf &
                 "username zte password zte privilege 15" & vbCrLf &
                 "clock timezone BeiJing 8" & vbCrLf &
                 "exit" & vbCrLf &
                 "clock set " & 时间 & " " & 月份 & " " & 日 & " " & 年 & vbCrLf & vbCrLf &
                 "con t" & vbCrLf &
                 "ntp enable" & vbCrLf &
                 "ntp client" & vbCrLf &
                 "ntp poll-interval 4" & vbCrLf &
                 "ntp server 132.232.5.161 priority 1 version 2" & vbCrLf &
                 "snmp-server community szxxjw view AllView rw" & vbCrLf &
                 "snmp-server host 132.232.5.161 version 2c szxxjw enable NOTIFICATIONS target-addr-name 1" & vbCrLf &
                 "snmp-server host 172.19.2.34 version 2c szxxjw enable NOTIFICATIONS target-addr-name 2" & vbCrLf &
                 "no snmp-server community public" & vbCrLf &
                 "no snmp-server community private" & vbCrLf &
                 "hostname " & hostname & vbCrLf & vbCrLf &
                 "vlan database" & vbCrLf &
                 "vlan 43,45,47,49," & 宽带起始VLAN & "-" & 宽带终止VLAN & ",4020,4050-4060" & vbCrLf &
                 "exit" & vbCrLf & vbCrLf &
                 "interface smartgroup1" & vbCrLf &
                 "smartgroup load-balance src-mac" & vbCrLf &
                 "smartgroup mode 802.3ad" & vbCrLf &
                 "switchport mode hybrid" & vbCrLf &
                 "switchport vlan 43,45,47,49," & 宽带起始VLAN & "-" & 宽带终止VLAN & ",4020,4050-4060 tag" & vbCrLf &
                 "exit" & vbCrLf & vbCrLf &
                 "interface xgei_1/10/1" & vbCrLf &
                 "phy-attribute lan" & vbCrLf &
                 "no shutdown" & vbCrLf &
                 "hybrid-attribute fiber" & vbCrLf &
                 "no negotiation auto" & vbCrLf &
                 "speed 10000" & vbCrLf &
                 "duplex full" & vbCrLf &
                 "flowcontrol disable" & vbCrLf &
                 "linktrap enable" & vbCrLf &
                 "switchport mode hybrid" & vbCrLf &
                 "switchport vlan 43,45,47,49," & 宽带起始VLAN & "-" & 宽带终止VLAN & ",4020,4050-4060 tag" & vbCrLf &
                 "smartgroup 1 mode active" & vbCrLf &
                 "port-protect disable" & vbCrLf &
                 "uplink-isolate disable" & vbCrLf &
                 "exit" & vbCrLf & vbCrLf &
                 "interface xgei_1/11/1" & vbCrLf &
                 "phy-attribute lan" & vbCrLf &
                 "no shutdown" & vbCrLf &
                 "hybrid-attribute fiber" & vbCrLf &
                 "no negotiation auto" & vbCrLf &
                 "speed 10000" & vbCrLf &
                 "duplex full" & vbCrLf &
                 "flowcontrol disable" & vbCrLf &
                 "linktrap enable" & vbCrLf &
                 "switchport mode hybrid" & vbCrLf &
                 "switchport vlan 43,45,47,49," & 宽带起始VLAN & "-" & 宽带终止VLAN & ",4020,4050-4060 tag" & vbCrLf &
                 "smartgroup 1 mode active" & vbCrLf &
                 "port-protect disable" & vbCrLf &
                 "uplink-isolate disable" & vbCrLf &
                 "exit" & vbCrLf & vbCrLf &
                 "con t" & vbCrLf &
                 "interface smartgroup1" & vbCrLf &
                 "port-protect disable" & vbCrLf &
                 "uplink-isolate disable" & vbCrLf &
                 "exit" & vbCrLf & vbCrLf &
                 "interface vlan 49" & vbCrLf &
                 "ip address " & ip地址 & " 255.255.255.252	" & vbCrLf &
                 "interface vlan 47" & vbCrLf &
                 "ip address " & 备用ip地址 & " 255.255.255.252	" & vbCrLf &
                 "!" & vbCrLf & vbCrLf &
                 "ip route 0.0.0.0 0.0.0.0 " & RouteIP & vbCrLf &
                 "ip route 9.240.0.0 255.255.0.0 " & 备用RouteIP & vbCrLf &
                 "ip route 19.0.0.0 255.0.0.0 " & 备用RouteIP & " 100" & vbCrLf &
                 "epon" & vbCrLf &
                 "onu-type-profile name ZTE-F820-1" & vbCrLf &
                 "onu-type-profile description ZTE-F820-1 24FE,32POTS" & vbCrLf &
                 "onu-type-profile interface eth_0/1-24 ZTE-F820-1" & vbCrLf &
                 "onu-type-profile interface pots_5/1-32 ZTE-F820-1" & vbCrLf &
                 "onu-type-profile name ZTE-F820-2" & vbCrLf &
                 "onu-type-profile description ZTE-F820-2 24FE,32pos" & vbCrLf &
                 "onu-type-profile interface eth_0/1-24 ZTE-F820-2" & vbCrLf &
                 "onu-type-profile interface pots_4/1-16 ZTE-F820-2" & vbCrLf &
                 "onu-type-profile interface pots_5/1-16 ZTE-F820-2" & vbCrLf &
                 "!" & vbCrLf &
                 "pon" & vbCrLf &
                 "onu-type MEP02 epon description 2FE,0POTS" & vbCrLf &
                 "onu-type MEP02 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type MEP12 epon description 2FE,1POTS" & vbCrLf &
                 "onu-type MEP12 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type MEP14 epon description 4FE,1POTS" & vbCrLf &
                 "onu-type MEP14 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type E8CG12 epon description 2FE,1POTS" & vbCrLf &
                 "onu-type E8CG12 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type E8CG24 epon description 4FE,2POTS" & vbCrLf &
                 "onu-type E8CG24 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type E8CP02 epon description 2FE,0POTS" & vbCrLf &
                 "onu-type E8CP02 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type E8CP11 epon description 1FE,1POTS" & vbCrLf &
                 "onu-type E8CP11 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type E8CP12 epon description 2FE,1POTS" & vbCrLf &
                 "onu-type E8CP12 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type E8CP14 epon description 4FE,1POTS" & vbCrLf &
                 "onu-type E8CP14 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type E8CP24 epon description 4FE,2POTS" & vbCrLf &
                 "onu-type E8CP24 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type EA0204 epon description 4FE,2POTS" & vbCrLf &
                 "onu-type EA0204 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type EA0404 epon description 4FE,4POTS" & vbCrLf &
                 "onu-type EA0404 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type EA0804 epon description 4FE,8POTS" & vbCrLf &
                 "onu-type EA0804 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type EA0808 epon description 8FE,8POTS" & vbCrLf &
                 "onu-type EA0808 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type EA1604 epon description 4FE,16POTS" & vbCrLf &
                 "onu-type EA1604 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type EA3204 epon description 4FE,32POTS" & vbCrLf &
                 "onu-type EA3204 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type ETVP12 epon description 2FE,1POTS" & vbCrLf &
                 "onu-type ETVP12 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type EWXP24 epon description 4FE,2POTS" & vbCrLf &
                 "onu-type GA0204 epon description 4FE,2POTS" & vbCrLf &
                 "onu-type GA0204 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type GA0404 epon description 4FE,4POTS" & vbCrLf &
                 "onu-type GA0404 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type GA0804 epon description 4FE,8POTS" & vbCrLf &
                 "onu-type GA0804 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type GA0808 epon description 8FE,8POTS" & vbCrLf &
                 "onu-type GA0808 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type GA1604 epon description 4FE,16POTS" & vbCrLf &
                 "onu-type GA1604 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type GA3204 epon description 4FE,32POTS" & vbCrLf &
                 "onu-type GA3204 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type E8C10P24 epon description 4FE,2POTS" & vbCrLf &
                 "onu-type E8C10P24 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type aWiFiP14 epon description 4FE,1POTS" & vbCrLf &
                 "onu-type aWiFiP14 epon speed 10g-asymmetric" & vbCrLf &
                 "onu-type-if ETVP12 eth_0/1-2" & vbCrLf &
                 "onu-type-if ETVP12 pots_0/1" & vbCrLf &
                 "onu-type-if EA3204 eth_0/1-4" & vbCrLf &
                 "onu-type-if EA3204 pots_0/1-32" & vbCrLf &
                 "onu-type-if GA1604 eth_0/1-4" & vbCrLf &
                 "onu-type-if GA1604 pots_0/1-16" & vbCrLf &
                 "onu-type-if EA1604 eth_0/1-4" & vbCrLf &
                 "onu-type-if EA1604 pots_0/1-16" & vbCrLf &
                 "onu-type-if MEP02 eth_0/1-2" & vbCrLf &
                 "onu-type-if MEP12 eth_0/1-2" & vbCrLf &
                 "onu-type-if MEP12 pots_0/1" & vbCrLf &
                 "onu-type-if MEP14 eth_0/1-4" & vbCrLf &
                 "onu-type-if MEP14 pots_0/1" & vbCrLf &
                 "onu-type-if E8CG12 eth_0/1-2" & vbCrLf &
                 "onu-type-if E8CG12 pots_0/1" & vbCrLf &
                 "onu-type-if GA0808 eth_0/1-8" & vbCrLf &
                 "onu-type-if GA0808 pots_0/1-8" & vbCrLf &
                 "onu-type-if GA0804 eth_0/1-4" & vbCrLf &
                 "onu-type-if GA0804 pots_0/1-8" & vbCrLf &
                 "onu-type-if GA0404 eth_0/1-4" & vbCrLf &
                 "onu-type-if GA0404 pots_0/1-4" & vbCrLf &
                 "onu-type-if GA0204 eth_0/1-4" & vbCrLf &
                 "onu-type-if GA0204 pots_0/1-2" & vbCrLf &
                 "onu-type-if EA0808 eth_0/1-8" & vbCrLf &
                 "onu-type-if EA0808 pots_0/1-8" & vbCrLf &
                 "onu-type-if EA0404 eth_0/1-4" & vbCrLf &
                 "onu-type-if EA0404 pots_0/1-4" & vbCrLf &
                 "onu-type-if EA0204 eth_0/1-4" & vbCrLf &
                 "onu-type-if EA0204 pots_0/1-2" & vbCrLf &
                 "onu-type-if E8CG24 eth_0/1-4" & vbCrLf &
                 "onu-type-if E8CG24 pots_0/1-2" & vbCrLf &
                 "onu-type-if E8CP02 eth_0/1-2" & vbCrLf &
                 "onu-type-if E8CP11 eth_0/1" & vbCrLf &
                 "onu-type-if E8CP11 pots_0/1" & vbCrLf &
                 "onu-type-if aWiFiP14 eth_0/1-4" & vbCrLf &
                 "onu-type-if aWiFiP14 pots_0/1" & vbCrLf &
                 "onu-type-if E8CP12 eth_0/1-2" & vbCrLf &
                 "onu-type-if E8CP12 pots_0/1" & vbCrLf &
                 "onu-type-if E8CP14 eth_0/1-4" & vbCrLf &
                 "onu-type-if E8CP14 pots_0/1" & vbCrLf &
                 "onu-type-if E8CP24 eth_0/1-4" & vbCrLf &
                 "onu-type-if E8CP24 pots_0/1-2" & vbCrLf &
                 "onu-type-if E8C10P24 eth_0/1-4" & vbCrLf &
                 "onu-type-if E8C10P24 pots_0/1-2" & vbCrLf &
                 "onu-type-if EA0804 eth_0/1-4" & vbCrLf &
                 "onu-type-if EA0804 pots_0/1-8" & vbCrLf &
                 "onu-type-if EWXP24 eth_0/1-4" & vbCrLf &
                 "onu-type-if EWXP24 pots_0/1-2" & vbCrLf & vbCrLf &
                 "!" & vbCrLf &
                 "igmp enable  " & vbCrLf &
                 "igmp mvlan 4020" & vbCrLf &
                 "igmp span-vlan enable" & vbCrLf &
                 "igmp mvlan 4020 priority 5" & vbCrLf &
                 "igmp mvlan 4020 enable" & vbCrLf &
                 "igmp mvlan 4020 work-mode proxy" & vbCrLf &
                 "igmp mvlan 4020 max-group 1024" & vbCrLf &
                 "igmp mvlan 4020 group 239.49.8.0 to 239.49.8.255" & vbCrLf &
                 "igmp mvlan 4020 group 239.49.0.0 to 239.49.0.255" & vbCrLf &
                 "igmp mvlan 4020 group 239.49.1.0 to 239.49.1.255" & vbCrLf &
                 "igmp mvlan 4020 group 239.49.9.0 to 239.49.9.255" & vbCrLf &
                 "igmp mvlan 4020 source-port smartgroup1" & vbCrLf &
                 "dhcpv4-l2-relay-agent enable" & vbCrLf &
                 "mvlan-translate 4020 to 43" & vbCrLf &
                 "port-identification rackno 1 frameno 1" & vbCrLf &
                 "port-identification access-node-id-type access-node-name" & vbCrLf &
                 "port-identification access-node-name " & ip地址 & vbCrLf & vbCrLf &
                 "configure t" & vbCrLf &
                 "file-server boot-server ftp ipaddress 132.232.5.161 user su3 password su3" & vbCrLf &
                 "exit" & vbCrLf & vbCrLf &
                 "file download version ETGOB.BT" & vbCrLf &
                 "file download version ETGOD.BT" & vbCrLf &
                 "file download version ETGOF.BT" & vbCrLf &
                 "file download version ETGO.FW" & vbCrLf &
                 "file download version ETGO.MVR" & vbCrLf & vbCrLf &
                 "file download version ETTO.BT" & vbCrLf &
                 "file download version ETTO.FW" & vbCrLf &
                 "file download version ETTO_LX.MVR" & vbCrLf &
                 "file download version ETXK_LX.MVR" & vbCrLf &
                 "file download version GTXK.BT" & vbCrLf &
                 "file download version ETXK.FW" & vbCrLf & vbCrLf &
                 "file download patch V210_p023.spat" & vbCrLf &
                 "patch active package v210_p023.spat" & vbCrLf &
                 "file download patch ETXKV210T46_r27.pat" & vbCrLf &
                 "patch active ETXKV210T46_r27.pat" & vbCrLf &
                 "file download patch ETTOV210T37_r27.pat" & vbCrLf &
                 "patch active ETTOV210T37_r27.pat" & vbCrLf & vbCrLf &
                 "write" & vbCrLf &
                 "reboot" & vbCrLf
    End Function
    Private Sub 新建重置_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新建重置.Click
        Hostname.Text = "XXXXX-ZX-OLT000X"
        C300新建起始VLAN.Text = "100X"
        C300新建终止VLAN.Text = "11XX"
        C300新建管理IP.Text = "9.42.XX.XX"
        新建代码.Text = ""
        宽带起始VLAN = ""
        宽带终止VLAN = ""
    End Sub
    Function chk_ip()
        '检查IP地址是否合法函数
        Dim boolIsIP, intLoop As Integer
        Dim arrIP
        Dim strip
        strip = C300新建管理IP.Text
        boolIsIP = 0  '函数初始值为true   
        arrIP = Split(strip, ".", -1, 1) '将输入的IP用"."分割为数组，数组下标从0开始，所以有效IP分割后的数组上界必须为3  
        If UBound(arrIP) <> 3 Then
            boolIsIP = 1
        Else
            For intLoop = 0 To UBound(arrIP)
                If Not IsNumeric(arrIP(intLoop)) Then       '检查数组元素中各项是否为数字，如果不是则不是有效IP   
                    boolIsIP = 2
                Else
                    If arrIP(intLoop) > 255 Or arrIP(intLoop) < 0 Then       '检查IP数字是否满足IP的取值范围   
                        boolIsIP = 3
                    End If
                End If
            Next
        End If
            chk_ip = boolIsIP
    End Function
End Class
