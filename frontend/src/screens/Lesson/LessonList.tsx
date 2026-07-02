import React, { useState, useCallback } from 'react';
import { TkButton } from '@takeoff-ui/react';
import { useListQuery } from '../../shared/useListQuery';
import { useDeleteMutation } from '../../shared/useDeleteMutation';
import { DeleteConfirmDialog } from '../../shared/DeleteConfirmDialog';
import { ListPageLayout } from '../../shared/ListPageLayout';
import type { TableColumn } from '../../shared/ListPageLayout';
import { actionColumn } from '../../shared/ActionButtons';
import { LessonCreate } from './LessonCreate';
import { LessonEdit } from './LessonEdit';

// ---------------------------------------------------------------------------
// Types
// ---------------------------------------------------------------------------

type LessonRecord = {
  id: string | number;
  lessonDate: string;
  dayOfWeek: string;
  startTime: string;
  endTime?: string;
  lessonType?: string;
  notes?: string;
  instructorId: string;
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
  { field: 'lessonDate', header: 'Lesson Date', sortable: true, filterType: 'datepicker', html: (row: Record<string, unknown>) => row.lessonDate ? new Date(String(row.lessonDate)).toLocaleDateString() : '—' },
  { field: 'dayOfWeek', header: 'Day Of Week', sortable: true, filterType: 'radio', filterOptions: [{ label: 'Monday', value: '0' }, { label: 'Tuesday', value: '1' }, { label: 'Wednesday', value: '2' }, { label: 'Thursday', value: '3' }, { label: 'Friday', value: '4' }, { label: 'Saturday', value: '5' }, { label: 'Sunday', value: '6' }], html: (row: Record<string, unknown>) => { const m: Record<string, string> = {'0': 'Monday', '1': 'Tuesday', '2': 'Wednesday', '3': 'Thursday', '4': 'Friday', '5': 'Saturday', '6': 'Sunday'}; return m[String(row.dayOfWeek ?? '')] ?? String(row.dayOfWeek ?? '\u2014'); } },
  { field: 'startTime', header: 'Start Time', sortable: true, searchable: true, filterType: 'text' },
  { field: 'endTime', header: 'End Time', sortable: true, searchable: true, filterType: 'text' },
  { field: 'lessonType', header: 'Lesson Type', sortable: true, searchable: true, filterType: 'text' },
  { field: 'notes', header: 'Notes', sortable: true, searchable: true, filterType: 'text' },
  { field: 'instructorId', header: 'Hoca', sortable: true },
];

// ---------------------------------------------------------------------------
// LessonList
// ---------------------------------------------------------------------------

export const LessonList: React.FC = () => {
  const list = useListQuery<LessonRecord>({ resource: 'Lesson' });
  const [showCreate, setShowCreate] = useState(false);
  const [editRecord, setEditRecord] = useState<LessonRecord | null>(null);
  const [selectedRows, setSelectedRows] = useState<LessonRecord[]>([]);

  const del = useDeleteMutation('Lesson');


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
        title="Lesson"
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
        onSelectionChange={(rows) => setSelectedRows(rows as LessonRecord[])}
        onCrudAction={handleCrudAction}
        headerActions={<>
          {selectedRows.length > 0 && (
            <TkButton label={`Delete (${selectedRows.length})`} variant="danger" onTkClick={() => del.requestDelete(selectedRows.map(r => r.id), `${selectedRows.length} record(s)`)} />
          )}

          <TkButton label="+ Create Lesson" variant="primary" onTkClick={() => setShowCreate(true)} />
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
      <LessonCreate open={showCreate} onClose={() => setShowCreate(false)} onSuccess={list.invalidate} />
      <LessonEdit record={editRecord} onClose={() => setEditRecord(null)} onSuccess={list.invalidate} />

    </>
  );
};

