import React, { useState, useCallback } from 'react';
import { TkButton } from '@takeoff-ui/react';
import { useListQuery } from '../../shared/useListQuery';
import { useDeleteMutation } from '../../shared/useDeleteMutation';
import { DeleteConfirmDialog } from '../../shared/DeleteConfirmDialog';
import { ListPageLayout } from '../../shared/ListPageLayout';
import type { TableColumn } from '../../shared/ListPageLayout';
import { actionColumn } from '../../shared/ActionButtons';
import { MemberCreate } from './MemberCreate';
import { MemberEdit } from './MemberEdit';

// ---------------------------------------------------------------------------
// Types
// ---------------------------------------------------------------------------

type MemberRecord = {
  id: string | number;
  firstName: string;
  lastName: string;
  email?: string;
  phone?: string;
  birthDate?: string;
  membershipStartDate: string;
  membershipEndDate?: string;
  membershipStatus: string;
  totalSessions: number;
  usedSessions: number;
  notes?: string;
  [key: string]: unknown;
};

// ---------------------------------------------------------------------------
// Column definition — edit this array to add/remove/reorder columns
// ---------------------------------------------------------------------------
//
// Override examples:
//   • Hide a column:     remove its entry from COLUMNS
//   • Add a custom col:  { field: 'fullName', header: 'Full Name', html: (row) => `${row.firstName} ${row.lastName}` }
//   • Enable filtering:  add searchable: true  or  filterType: 'text' | 'checkbox' | 'radio' | 'datepicker'
//   • Custom cell render: html: (row) => `<span style="color:green">${row.status}</span>`
//
// Shared components (src/shared/) can be edited to change behavior globally:
//   • ListPageLayout  — table wrapper, pagination, header layout
//   • ActionButtons   — edit/delete button styles, labels, and behavior
//   • useListQuery    — data fetching, sorting, filtering logic
//   • useDeleteMutation / DeleteConfirmDialog — delete flow
//
// Action buttons override: edit src/shared/ActionButtons.ts DEFAULT_CONFIG
// or pass custom config: actionColumn('id', { hasEdit: true, config: { edit: { label: 'View', style: '...' } } })
//

const COLUMNS: TableColumn[] = [
  { field: 'id', header: 'ID', sortable: true },
  { field: 'firstName', header: 'First Name', sortable: true, searchable: true, filterType: 'text' },
  { field: 'lastName', header: 'Last Name', sortable: true, searchable: true, filterType: 'text' },
  { field: 'email', header: 'Email', sortable: true, searchable: true, filterType: 'text' },
  { field: 'phone', header: 'Phone', sortable: true, searchable: true, filterType: 'text' },
  { field: 'birthDate', header: 'Birth Date', sortable: true, filterType: 'datepicker', html: (row: Record<string, unknown>) => row.birthDate ? new Date(String(row.birthDate)).toLocaleDateString() : '—' },
  { field: 'membershipStartDate', header: 'Membership Start Date', sortable: true, filterType: 'datepicker', html: (row: Record<string, unknown>) => row.membershipStartDate ? new Date(String(row.membershipStartDate)).toLocaleDateString() : '—' },
  { field: 'membershipEndDate', header: 'Membership End Date', sortable: true, filterType: 'datepicker', html: (row: Record<string, unknown>) => row.membershipEndDate ? new Date(String(row.membershipEndDate)).toLocaleDateString() : '—' },
  { field: 'membershipStatus', header: 'Membership Status', sortable: true, filterType: 'radio', filterOptions: [{ label: 'Active', value: '0' }, { label: 'Passive', value: '1' }, { label: 'Frozen', value: '2' }], html: (row: Record<string, unknown>) => { const m: Record<string, string> = {'0': 'Active', '1': 'Passive', '2': 'Frozen'}; return m[String(row.membershipStatus ?? '')] ?? String(row.membershipStatus ?? '\u2014'); } },
  { field: 'totalSessions', header: 'Total Sessions', sortable: true },
  { field: 'usedSessions', header: 'Used Sessions', sortable: true },
  { field: 'notes', header: 'Notes', sortable: true, searchable: true, filterType: 'text' },
];

// ---------------------------------------------------------------------------
// MemberList
// ---------------------------------------------------------------------------

export const MemberList: React.FC = () => {
  const list = useListQuery<MemberRecord>({ resource: 'Member' });
  const [showCreate, setShowCreate] = useState(false);
  const [editRecord, setEditRecord] = useState<MemberRecord | null>(null);
  const [selectedRows, setSelectedRows] = useState<MemberRecord[]>([]);

  const del = useDeleteMutation('Member');


  const columns = [...COLUMNS, ...actionColumn('id', { hasEdit: true, hasDelete: true })];

  const handleCrudAction = useCallback((action: string, id: string) => {
    const row = list.records.find((r) => String(r.id) === id);
    if (!row) return;
    if (action === 'edit') setEditRecord(row);
    if (action === 'delete') del.requestSingleDelete(row.id);
  }, [list.records]);

  return (
    <>
      <ListPageLayout
        title="Member"
        subtitle={Object.keys(list.displayParams).length > 0 ? (
          <div style={{ fontSize: 13, color: '#666', marginTop: 4 }}>
            {Object.entries(list.displayParams).map(([k, v]) => (
              <span key={k} style={{ marginRight: 12 }}>{k}: <strong>{v}</strong></span>
            ))}
          </div>
        ) : undefined}
        records={list.records}
        columns={columns}
        dataKey="id"
        total={list.total}
        loading={list.isLoading}
        page={list.page}
        perPage={list.perPage}
        onPageChange={list.setPage}
        onPerPageChange={list.setPerPage}
        onTableRequest={list.handleTableRequest}
        selectionMode="checkbox"
        selectedRows={selectedRows}
        onSelectionChange={(rows) => setSelectedRows(rows as MemberRecord[])}
        onCrudAction={handleCrudAction}
        headerActions={<>
          {selectedRows.length > 0 && (
            <TkButton label={`Delete (${selectedRows.length})`} variant="danger" onTkClick={() => del.requestDelete(selectedRows.map(r => r.id), `${selectedRows.length} record(s)`)} />
          )}

          <TkButton label="+ Create Member" variant="primary" onTkClick={() => setShowCreate(true)} />
        </>}
      />

      {list.isError && (
        <div style={{ padding: '10px 14px', background: '#fff3f3', border: '1px solid #f5c6c6', borderRadius: 6, color: '#c62828', fontSize: 13, marginBottom: 12 }}>
          Failed to load data: {(list.error as Error).message}
        </div>
      )}

      <DeleteConfirmDialog
        visible={!!del.deleteTarget}
        label={del.deleteTarget?.label ?? ''}
        isPending={del.isPending}
        onConfirm={del.confirmDelete}
        onCancel={() => del.setDeleteTarget(null)}
      />
      <MemberCreate open={showCreate} onClose={() => setShowCreate(false)} onSuccess={list.invalidate} />
      <MemberEdit record={editRecord} onClose={() => setEditRecord(null)} onSuccess={list.invalidate} />

    </>
  );
};

